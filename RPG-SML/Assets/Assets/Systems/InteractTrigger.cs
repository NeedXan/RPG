using Conversa.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{

    [SerializeField] bool isDialogue;

    public bool canInteract;
    public bool canInteractTemp;

    bool playerInTrigger;

    private void Awake()
    {
        canInteractTemp = canInteract;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Set player to interactable state
            playerInTrigger = true;
        }
    }

    private void LateUpdate()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E) && canInteractTemp)
        {
            if (isDialogue)
            {
                canInteractTemp = false;

                TopDownPlayerMovement.instance.EnterDialogue();

                gameObject.GetComponent<ConversaController>().Begin();
            }
        }
    }
}
