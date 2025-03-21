using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public int questNumber;
    public QuestManager qm;

    public string startQuestText;
    public string finishQuestText;

    public bool isItemQuest;
    public string targetItem;
    public bool isEnemyQuest;
    public string targetEnemy;
    public int enemiesToKill;
    public int enemyKillCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isItemQuest) 
        {
            if (qm.itemCollected == targetItem)
            {
                qm.itemCollected = null;
                FinishQuest();
            }
        }

        if (isEnemyQuest)
        {
            if (qm.itemCollected == targetEnemy) 
            {
                qm.enemyKilled = null;
                enemyKillCount++;
            }

            if (enemyKillCount >= enemiesToKill) 
            { 
            FinishQuest();
            }
        }
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
