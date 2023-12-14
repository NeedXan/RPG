using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{

    [SerializeField] bool isDialogue;

    public bool canInteract;
    [HideInInspector] public bool canInteractTemp;

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
                
            }
        }
    }
}
