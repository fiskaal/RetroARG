using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    [SerializeField]private DialogueManager dm;
    [SerializeField] private bool isDialogueOpen = false;
    [SerializeField] private bool inTrigger = false;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;
    public Animator npcAnim;

    public string[] dialogueLines;
    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openButton.gameObject.SetActive(true);

            if (Input.GetButtonDown("Dialogue"))
            {

                isDialogueOpen = true;

                if (!dm.dialogueActive)
                {
                    dm.dialogueLines = dialogueLines;
                    dm.currentLine = 0;
                    dm.ShowDialogue();
                    npcAnim.SetBool("isTalking", true);
                }
            }
            inTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dm.HideWindow(dialogue);
            openButton.gameObject.SetActive(false);
            dm.HideDialogue();
            inTrigger = false;
            isDialogueOpen = false;
            npcAnim.SetBool("isTalking", false);

        }
    }
}
