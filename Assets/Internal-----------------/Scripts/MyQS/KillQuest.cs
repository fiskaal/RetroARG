using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillQuest : MonoBehaviour
{
    private enum QuestState
    {
        NotTaken, Active, Completed, Default
    }
    [Header("Quest States")]
    [SerializeField] private QuestState currentState;
    //[SerializeField] private GameObject notification;
    [SerializeField] private Animator notifAnim;
    [SerializeField] private TMP_Text notifText;
    [SerializeField] private string notifStartedText;
    [SerializeField] private string notifCompleteText;
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
    public GameObject killQuestInfo;
    public GameObject enemy;
    [Header("Player attack script")]
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
        killQuestInfo.SetActive(false);
        pa.enemiesKilled = enemiesKilled;
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
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesStart.Length - 1) && Input.GetButtonDown("Triangle"))
                    {
                        Debug.Log("Kill quest started");
                        
                        notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                        currentState = QuestState.Active;
                    }
                    break;

                case QuestState.Active:
                    dm.dialogueLines = dialogueLinesActive;
                    enemy.SetActive(true);
                    if (pa.enemiesKilled > 0)
                    {
                        currentState = QuestState.Completed;
                        Debug.Log("Kill quest goal reached");
                    }
                    break;

                case QuestState.Completed:
                    dm.dialogueLines = dialogueLinesCompleted;
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesCompleted.Length - 1) && Input.GetButtonDown("Triangle"))
                    {
                        Debug.Log("Kill quest complete");
                        currentState = QuestState.Default;
                        notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                        rewardItem.SetActive(true);
                    }

                    break;

                case QuestState.Default:
                    dm.dialogueLines = dialogueLinesDefault;

                    break;
            }
        }
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
