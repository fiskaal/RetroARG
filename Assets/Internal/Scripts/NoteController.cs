using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class NoteController : MonoBehaviour
{

    //[Header("Input")]
    //[SerializeField] private KeyCode closekey;
    [Header("Use this to capture inputs")] public Inputs PlayerInputs;
    private bool _submit;

    [Header("UI text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextAreaUI;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;

    [Space(10)]
    [SerializeField] [TextArea] private string noteText;

    [Space(10)]
   
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool inTrigger = false;

    private string noteName;
    //[SerializeField] private PauseMenu pauseMenu;

    public void Awake()
    {
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        noteCanvas.gameObject.SetActive(false);
        isOpen = false;
    }
    private void Update()
    {
        if (inTrigger == true && Input.GetButton("Submit"))
        {
            ShowNote();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            HideNote();
        }
    }



    public void ShowNote()
    {
        Debug.Log("Note opened");
        isOpen = true;
        //ShowNote();
        noteCanvas.gameObject.SetActive(true);
        noteTextAreaUI.text = noteText;
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        Time.timeScale = .0f;
    }

    void HideNote()
    {
        noteCanvas.gameObject.SetActive(false);
        isOpen = false;
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        Time.timeScale = 1.0f;

    }   
    IEnumerator NoteClosed()
    {
        yield return new WaitForSeconds(0.1F);
        //pauseMenu.noteOpened = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
            {
            inTrigger = true;
            if (!isOpen)
            {
                openButton.gameObject.SetActive(true);
                if (Input.GetButton("Submit"))
                {
                    ShowNote();
                }
            }else if(isOpen == true)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    HideNote();
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
