using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Animator animator;

    public Image npcImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool isDialogueOpen = false;

    private Queue<string> sentences;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Error message");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueOpen = true;
        animator.SetBool("isOpen", true);

        npcImage.sprite = dialogue.npcSprite;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        isDialogueOpen = false;
        animator.SetBool("isOpen", false);
    }
}
