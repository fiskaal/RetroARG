using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueWindow;
    public TMP_Text dialogueText;

    public bool dialogueActive = false;

    public string[] dialogueLines;
    public int currentLine;

    // Start is called before the first frame update
    void Start()
    {
        dialogueWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetButtonDown("Interact"))
        {
            
            currentLine++;
        }

        if (currentLine >= dialogueLines.Length) 
        {
            dialogueWindow.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
        }

        dialogueText.text = dialogueLines[currentLine];
    }

    public void ShowWindow(string dialogue)
    {
        dialogueActive=true;
        dialogueWindow.SetActive(true);
        dialogueText.text = dialogue;
        
    }
    public void HideWindow(string dialogue)
    {
        dialogueActive = false;
        dialogueWindow.SetActive(false);
        dialogueText.text = dialogue;
        currentLine = 0;

    }

    public void ShowDialogue()
    {
        dialogueActive=true;
        dialogueWindow.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueActive = false;
        dialogueWindow.SetActive(false);
    }
}
