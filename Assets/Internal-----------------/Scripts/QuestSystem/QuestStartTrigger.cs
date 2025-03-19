using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartTrigger : MonoBehaviour
{
    [SerializeField] private QuestManager qm;
    public int questNumber;
    public bool startQuest;
    public bool finishQuest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            if (!qm.questCompleted[questNumber])
            {
                if (startQuest && !qm.quests[questNumber].gameObject.activeSelf) 
                {
                    qm.quests[questNumber].gameObject.SetActive(true);
                    qm.quests[questNumber].StartQuest();
                }

                
            }
        }
    }
}
