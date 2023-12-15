using Conversa.Runtime.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject messageWindow;
    [SerializeField] private GameObject choiceWindow;
    [SerializeField] private TextMeshProUGUI actorNameText;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private GameObject choiceOptionButtonPrefab;

    private Action onSpaceKeyDown;

    public void ShowMessage(string actor, string message, Action onContinue)
    {
        choiceWindow.SetActive(false);
        messageWindow.SetActive(true);

        actorNameText.text = actor;
        messageText.text = message;

        onSpaceKeyDown = onContinue;
    }

    public void ShowChoice(string actor, string message, List<Option> options)
    {
        messageWindow.SetActive(true);

        actorNameText.text = actor;
        messageText.text = message;

        choiceWindow.SetActive(true);

        foreach (Transform child in choiceWindow.transform)
            Destroy(child.gameObject);

        options.ForEach(option =>
        {
            var instance = Instantiate(choiceOptionButtonPrefab, Vector3.zero, Quaternion.identity);
            instance.transform.SetParent(choiceWindow.transform);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = option.Message;
            instance.GetComponent<Button>().onClick.AddListener(() => option.Advance());
        });
    }

    public void Hide()
    {
        messageWindow.SetActive(false);
        choiceWindow.SetActive(false);
        onSpaceKeyDown = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onSpaceKeyDown != null)
        {
            onSpaceKeyDown();
        }
    }
}
