using Conversa.Runtime.Interfaces;
using Conversa.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Conversa.Runtime.Events;

public class ConversaController : MonoBehaviour
{
    [SerializeField] private Conversation conversation;
    [SerializeField] private UIController uiController;

    private ConversationRunner runner;

    private void Start()
    {
        runner = new ConversationRunner(conversation);
        runner.OnConversationEvent.AddListener(HandleConversationEvent);
    }

    public void Begin()
    {
        runner.Begin();
    }


    private void HandleConversationEvent(IConversationEvent e)
    {
        switch (e)
        {
            case MessageEvent messageEvent:
                HandleMessage(messageEvent);
                break;
            case ChoiceEvent choiceEvent:
                HandleChoice(choiceEvent);
                break;
            case ActorMessageEvent actorMessageEvent:
                HandleActorMessageEvent(actorMessageEvent);
                break;
            case ActorChoiceEvent actorChoiceEvent:
                HandleActorChoiceEvent(actorChoiceEvent);
                break;
            case UserEvent userEvent:
                HandleUserEvent(userEvent);
                break;
            case EndEvent _:
                HandleEnd();
                break;
        }
    }

    private void HandleMessage(MessageEvent e)
    {
        uiController.ShowMessage(e.Actor, e.Message, () => e.Advance());
    }

    private void HandleChoice(ChoiceEvent e)
    {
        uiController.ShowChoice(e.Actor, e.Message, e.Options);
    }

    private void HandleActorMessageEvent(ActorMessageEvent evt)
    {
        uiController.ShowMessage(evt.Actor.DisplayName, evt.Message, evt.Advance);
    }

    private void HandleActorChoiceEvent(ActorChoiceEvent evt)
    {
        uiController.ShowChoice(evt.Actor.DisplayName, evt.Message, evt.Options);
    }

    private static void HandleUserEvent(UserEvent userEvent)
    {
    }

    private void HandleEnd() => uiController.Hide();
}
