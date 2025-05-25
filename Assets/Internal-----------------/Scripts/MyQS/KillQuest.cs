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
    [SerializeField] private AudioSource questStart;
    [SerializeField] private AudioSource questComplete;
    [Header("Dialogue Details")]
    [SerializeField] private DialogueHolder dh;
    public string dialogue;
    [SerializeField] private GameObject killQuestIcon;
    [SerializeField]private DialogueManager dm;
    public string[] dialogueLines;
    public string[] dialogueLinesStart;
    public string[] dialogueLinesActive;
    public string[] dialogueLinesCompleted;
    public string[] dialogueLinesDefault;
    public int arrayLenght;
    public int currentIndex;
    public bool isKillTrigger = false;
    public GameObject killQuestInfo;
    [Header("Player attack script")]
    public PlayerAttack pa;

    [Header("Quest Details")]
    [SerializeField] private int enemiesToKill;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private GameObject rewardItem;
    [SerializeField] private GameObject enemyToKill;
    // Start is called before the first frame update
    void Start()
    {
        arrayLenght = dialogueLinesStart.Length;
        rewardItem.SetActive(false);
        //isKillTrigger = false;
        killQuestInfo.SetActive(false);
        killQuestIcon.SetActive(false);
        enemyToKill.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isKillTrigger)
        {
            dm.dialogueIcon = killQuestIcon;
            switch (currentState)
            {

                case QuestState.NotTaken:
                    dm.dialogueLines = dialogueLinesStart;
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesStart.Length - 1) && Input.GetButtonDown("Square"))
                    {
                        Debug.Log("Kill quest started");
                        
                        notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                        questStart.Play();
                        enemyToKill.SetActive(true);
                        killQuestInfo.SetActive(true);
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
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesCompleted.Length - 1) && Input.GetButtonDown("Square"))
                    {
                        Debug.Log("Kill quest complete");
                        currentState = QuestState.Default;
                        notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                        questComplete.Play();
                        rewardItem.SetActive(true);
                    }

                    break;

                case QuestState.Default:
                    dm.dialogueLines = dialogueLinesDefault;

                    break;
            }
        }
        else
        {
            
        }
    }

    

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("KillQuest"))
        {
            isKillTrigger = true;
            //if (isKillTrigger) { dm.dialogueIcon = killQuestIcon; }
            //killQuestIcon.SetActive(true);
            

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("KillQuest"))
        {
            isKillTrigger = false;
            killQuestIcon.SetActive(false);
            dm.dialogueIcon = dm.defaultIcon;
        }
    }

}
