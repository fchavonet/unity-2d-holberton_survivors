using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    // The dialogue to be triggered
    public Dialogue dialogue;

    // Boolean to track if the player is in range
    public bool isInRange;

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is the player
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collider is the player
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    // Trigger the dialogue to start
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
