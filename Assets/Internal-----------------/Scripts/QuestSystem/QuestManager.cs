
using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests;
    public bool[] questCompleted;

    public DialogueManager dm;

    public string itemCollected;
    // Start is called before the first frame update
    void Start()
    {
        questCompleted = new bool[quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText)
    {
        dm.dialogueLines = new string[1];
        dm.dialogueLines[0] = questText;
        dm.currentLine = 0;
        dm.ShowDialogue();
    }
}
