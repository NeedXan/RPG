using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InText : MonoBehaviour
{
    public TopDownPlayerMovement playerScript;

    public InteractTrigger[] triggersToDisable;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownPlayerMovement>();
    }

    // Start is called before the first frame update
    public void StartDialogue()
    {
        Debug.Log("Starting Dialogue");
        playerScript._canMove = false;

        foreach (var trigger in triggersToDisable)
        {
            trigger.canInteractTemp = false;
        }
    }

    // Update is called once per frame
    public void EndDialogue()
    {
        playerScript._canMove = true;
        foreach (var trigger in triggersToDisable)
        {
            trigger.canInteractTemp = true;
        }
    }
}
