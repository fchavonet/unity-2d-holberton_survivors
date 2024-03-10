using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Singleton instance of the DialogueManager
    public static DialogueManager instance;

    // Animator for handling dialogue box animations
    public Animator animator;

    // Image displaying the NPC's sprite
    public Image npcImage;
    // Text displaying the NPC's name
    public TMP_Text nameText;
    // Text displaying the dialogue
    public TMP_Text dialogueText;

    // Boolean to track if a dialogue is currently open
    public bool isDialogueOpen = false;

    // Queue to hold sentences for the dialogue
    private Queue<string> sentences;

    private void Awake()
    {
        // Ensure there's only one instance of DialogueManager
        if (instance != null)
        {
            Debug.LogWarning("Error message");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    // Start a new dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueOpen = true;
        animator.SetBool("isOpen", true);

        // Set NPC image and name
        npcImage.sprite = dialogue.npcSprite;
        nameText.text = dialogue.name;

        // Clear any existing sentences
        sentences.Clear();

        // Enqueue all sentences in the dialogue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Display the first sentence
        DisplayNextSentence();
    }

    // Display the next sentence in the dialogue
    public void DisplayNextSentence()
    {
        // Check if there are no more sentences
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Get the next sentence
        string sentence = sentences.Dequeue();
        // Stop any previous typing coroutine and start typing the new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Coroutine to type out the sentence letter by letter
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        // Iterate through each letter in the sentence
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // Typing speed
            yield return new WaitForSeconds(0.05f);
        }
    }

    // End the dialogue
    void EndDialogue()
    {
        isDialogueOpen = false;
        animator.SetBool("isOpen", false);
    }
}
