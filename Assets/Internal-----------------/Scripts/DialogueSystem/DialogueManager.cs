using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueWindow;
    public TMP_Text dialogueText;

    public bool dialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (dialogueActive && Input.GetButtonDown("Dialogue"))
        //{
        //    dialogueWindow.SetActive(false);
        //    dialogueActive = false;
        //}
    }

    public void ShowWindow(string dialogue)
    {
        dialogueActive=true;
        dialogueWindow.SetActive(true);
        dialogueText.text = dialogue;
        
    }
    public void HideWindow(string dialogue)
    {
        dialogueWindow.SetActive(false);
        
    }
}
