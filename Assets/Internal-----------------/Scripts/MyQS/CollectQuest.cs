using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : Quest
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
    //public PlayerAttack pa;

    [Header("Quest Details")]
    [SerializeField] private int amountToCollect;
    [SerializeField] private int amountCollected;
    [SerializeField] private GameObject rewardItem;
    // Start is called before the first frame update
    void Start()
    {
        arrayLenght = dialogueLinesStart.Length;
        rewardItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case QuestState.NotTaken:
                dm.dialogueLines = dialogueLinesStart;
                if ((dm.currentLine == dialogueLinesStart.Length - 1) && Input.GetButtonDown("Dialogue"))
                {
                    Debug.Log("Collect quest started");
                    currentState = QuestState.Active;
                }
                break;

            case QuestState.Active:
                dm.dialogueLines = dialogueLinesActive;
                //if(pa.enemiesKilled > 0)
                //{
                //    currentState = QuestState.Completed;
                //    Debug.Log("Collect quest goal reached");
                //}
                break;

            case QuestState.Completed:
                dm.dialogueLines = dialogueLinesCompleted;
                if ((dm.currentLine == dialogueLinesCompleted.Length - 1) && Input.GetButtonDown("Dialogue"))
                {
                    Debug.Log("Collect quest complete");
                    currentState = QuestState.Default;
                    rewardItem.SetActive(true);
                }

                break;

            case QuestState.Default:
                dm.dialogueLines = dialogueLinesDefault;

                break;
        }
    }

    public override void ProcessDialogue()
    {
        Debug.Log("New process dialogue");
        //nastavit activated dialogue array
    }
}
