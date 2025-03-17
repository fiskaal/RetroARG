using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string dialogue;
    private DialogueManager dm;
    [SerializeField] private bool isDialogueOpen = false;
    [SerializeField] private bool inTrigger = false;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;
    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger == true && Input.GetButton("Dialogue"))
        {
            dm.ShowWindow(dialogue);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            dm.HideWindow(dialogue);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player")) 
    //    {
    //        if (Input.GetButtonUp("Dialogue"))
    //        {
    //            dm.ShowWindow(dialogue);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            inTrigger = true;
            if (!isDialogueOpen)
            {
                openButton.gameObject.SetActive(true);
                if (Input.GetButtonDown("Dialogue"))
                {
                    dm.ShowWindow(dialogue);
                }
            }
            else if (isDialogueOpen == true)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    dm.HideWindow(dialogue);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openButton.gameObject.SetActive(false);
            inTrigger = false;

        }
    }
}
