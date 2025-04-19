using System.Collections;
using TMPro;
using UnityEngine;


public class NoteController : MonoBehaviour
{
    [Header("UI text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextAreaUI;
    [SerializeField] private GameObject openButton;
    [SerializeField] private TMP_Text openText;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private TMP_Text closeText;

    [Space(10)]
    [SerializeField][TextArea] private string noteText;

    [Space(10)]

    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool inTrigger = false;

    private string noteName;
    //[SerializeField] private PauseMenu pauseMenu;

    public void Start()
    {
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        noteCanvas.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (inTrigger && Input.GetButtonDown("Interact"))
        {
            isOpen = true;
            ShowNote();
        }

        if (isOpen && Input.GetButtonDown("Cancel"))
        {
            HideNote();
        }
        
    }



    public void ShowNote()
    {
        
        Debug.Log("Note opened");
        noteCanvas.gameObject.SetActive(true);
        noteTextAreaUI.text = noteText;
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        closeText.text = "CLOSE";
        //Time.timeScale = 0f;
    }

    void HideNote()
    {
        inTrigger = false;
        isOpen = false;
        Debug.Log("Note closed");
        noteCanvas.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        //Time.timeScale = 1f;

    }
    IEnumerator NoteClosed()
    {
        yield return new WaitForSeconds(0.1F);
        //pauseMenu.noteOpened = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
            isOpen = false;
            openButton.gameObject.SetActive(false);
            HideNote();
           

        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = true;
            openButton.gameObject.SetActive(true);
            openText.text= "OPEN";

            
        }
    }


}
