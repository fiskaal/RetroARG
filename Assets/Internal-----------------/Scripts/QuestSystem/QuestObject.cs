using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public int questNumber;
    public QuestManager qm;

    public string startQuestText;
    public string finishQuestText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuest()
    {
        qm.ShowQuestText(startQuestText);
    }

    public void FinishQuest()
    {
        qm.ShowQuestText(finishQuestText);
        qm.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
