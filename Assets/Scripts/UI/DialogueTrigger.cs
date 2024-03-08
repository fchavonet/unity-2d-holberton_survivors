using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //public GameObject interactUI;

    public bool isInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            //interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            //interactUI.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
