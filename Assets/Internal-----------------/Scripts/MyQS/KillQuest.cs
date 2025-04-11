using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    //enum QuestState
    //{
    //    NotTaken, Active, Completed
    //}
    //[Header("Quest States")]
    //[SerializeField] private QuestState currentState;

    [Header("Dialogue Details")]
    [SerializeField] private DialogueHolder dh;
    public string dialogue;
    [SerializeField]private DialogueManager dm;
    public string[] dialogueLines;
    public string[] dialogueLinesStart;
    public string[] dialogueLinesActive;
    public string[] dialogueLinesCompleted;
    public string[] dialogueLinesDefault;
    public int arrayLenght;
    public int currentIndex;
    public bool isKillTrigger;
    [Header("PLayer attack script")]
    public PlayerAttack pa;

    [Header("Quest Details")]
    [SerializeField] private int enemiesToKill;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private GameObject rewardItem;
    // Start is called before the first frame update
    void Start()
    {
        arrayLenght = dialogueLinesStart.Length;
        rewardItem.SetActive(false);
        isKillTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKillTrigger == true)
        {
            switch (currentState)
            {
                case QuestState.NotTaken:
                    dm.dialogueLines = dialogueLinesStart;
                    if ((dm.currentLine == dialogueLinesStart.Length - 1) && Input.GetButtonDown("Interact"))
                    {
                        Debug.Log("Kill quest started");
                        currentState = QuestState.Active;
                    }
                    break;

                case QuestState.Active:
                    dm.dialogueLines = dialogueLinesActive;
                    if (pa.enemiesKilled > 0)
                    {
                        currentState = QuestState.Completed;
                        Debug.Log("Kill quest goal reached");
                    }
                    break;

                case QuestState.Completed:
                    dm.dialogueLines = dialogueLinesCompleted;
                    if ((dm.currentLine == dialogueLinesCompleted.Length - 1) && Input.GetButtonDown("Interact"))
                    {
                        Debug.Log("Kill quest complete");
                        currentState = QuestState.Default;
                        rewardItem.SetActive(true);
                    }

                    break;

                case QuestState.Default:
                    dm.dialogueLines = dialogueLinesDefault;

                    break;
            }
        }
    }

    public override void ProcessDialogue()
    {
        Debug.Log("New process dialogue");
        //nastavit activated dialogue array
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KillQuest"))
        {
            isKillTrigger = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KillQuest"))
        {
            isKillTrigger = false;
        }
    }

}
