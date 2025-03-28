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
    private DialogueManager dm;
    public string[] dialogueLines;
    public string[] dialogueLinesStart;
    public string[] dialogueLinesActive;
    public string[] dialogueLinesCompleted;

    [Header("Quest Details")]
    [SerializeField] private int enemiesToKill;
    [SerializeField] private int enemiesKilled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case QuestState.NotTaken:

                break;

            case QuestState.Active:

                break;

            case QuestState.Completed:

                break;
        }
    }

    public override void ProcessDialogue()
    {
        Debug.Log("New process dialogue");
        //nastavit activated dialogue array
    }
}
