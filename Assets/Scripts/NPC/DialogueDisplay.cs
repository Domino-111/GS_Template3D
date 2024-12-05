using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public static DialogueDisplay Main;

    public GameObject dialogueBox;

    public Text dialogueText;
    public Text nameText;

    public string[] currentDialogue;

    public int currentIndex;

    public bool dialogueActive;

    private void Awake()
    {
        Main = this;
    }

    public void StartDialogue(string name, string[] dialogue)
    {
        Time.timeScale = 0;

        dialogueBox.SetActive(true);

        nameText.text = name;
        currentDialogue = dialogue;

        dialogueText.text = currentDialogue[0];
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialogueActive = true;
    }

    public void ContinueDialogue()
    {
        currentIndex++;

        if (currentIndex >= currentDialogue.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = currentDialogue[currentIndex];
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);

        currentIndex = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogueActive = false;

        Time.timeScale = 1;
    }
}
