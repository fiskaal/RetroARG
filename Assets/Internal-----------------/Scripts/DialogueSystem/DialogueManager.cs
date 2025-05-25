using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueWindow;
    public TMP_Text dialogueText;
    public GameObject? dialogueIcon = null;
    public GameObject defaultIcon;

    public bool dialogueActive = false;

    public string[] dialogueLines;
    public int currentLine;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        dialogueWindow.SetActive(false);
        dialogueIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetButtonDown("Square"))
        {
            
            currentLine++;
        }

        if (currentLine >= dialogueLines.Length) 
        {
            dialogueWindow.SetActive(false);
            dialogueActive = false;
            dialogueIcon.SetActive(false);
            defaultIcon.SetActive(false);

            currentLine = 0;
        }

        dialogueText.text = dialogueLines[currentLine];
    }

    public void ShowWindow(string dialogue)
    {
        dialogueActive=true;
        
        dialogueWindow.SetActive(true);
        dialogueIcon.SetActive(true);
        dialogueText.text = dialogue;
        defaultIcon.SetActive(true);


    }
    public void HideWindow(string dialogue)
    {
        dialogueActive = false;
        
        dialogueWindow.SetActive(false);
        dialogueIcon.SetActive(false);
        dialogueText.text = dialogue;
        currentLine = 0;
        defaultIcon.SetActive(false);

    }

    public void ShowDialogue()
    {
        dialogueActive=true;
        dialogueWindow.SetActive(true);
        dialogueIcon.SetActive(true);
        defaultIcon.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueActive = false;
        dialogueWindow.SetActive(false);
        dialogueIcon.SetActive(false);
        defaultIcon.SetActive(false);
    }
}
