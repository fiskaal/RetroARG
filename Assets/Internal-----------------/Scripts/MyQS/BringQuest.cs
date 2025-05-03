using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BringQuest : MonoBehaviour
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
    [SerializeField] private GameObject dialogueIcon;
    [SerializeField] private DialogueManager dm;
    public string[] dialogueLines;
    public string[] dialogueLinesStart;
    public string[] dialogueLinesActive;
    public string[] dialogueLinesCompleted;
    public string[] dialogueLinesDefault;
    public int arrayLenght;
    public int currentIndex;
    public bool isCollectTrigger;
    public GameObject collectQuestInfo;
    [Header("Player attack script")]
    public PlayerAttack pa;

    [Header("Quest Details")]
    [SerializeField] private int objectsToBring;
    [SerializeField] private int objectsBrought;
    [SerializeField] private GameObject rewardItem;
    [SerializeField] private GameObject objectToBring;
    

    [Header("Collect Object Details")]
    [SerializeField] private bool isCarryingObject;
    public Rigidbody rb;
    public Collider coll;
    public bool carrySlotFull;
    [SerializeField]  private bool inTrigger;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform carryPos;
    [SerializeField] private GameObject actionButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        arrayLenght = dialogueLinesStart.Length;
        rewardItem.SetActive(false);
        isCollectTrigger = false;
        collectQuestInfo.SetActive(false);
        dialogueIcon.SetActive(false);
        objectToBring.SetActive(false);

        //Setup
        if (!isCarryingObject)
        {

            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (isCarryingObject)
        {

            rb.isKinematic = true;
            coll.isTrigger = true;
            carrySlotFull = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollectTrigger)
        {
            dm.dialogueIcon = dialogueIcon;
            switch (currentState)
            {
                case QuestState.NotTaken:
                    dm.dialogueLines = dialogueLinesStart;
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesStart.Length - 1) && Input.GetButtonDown("Triangle"))
                    {
                        Debug.Log("Collect quest started");

                        notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                        questStart.Play();
                        collectQuestInfo.SetActive(true);
                        objectToBring.SetActive(true);
                        currentState = QuestState.Active;
                    }
                    break;

                case QuestState.Active:
                    dm.dialogueLines = dialogueLinesActive;
                    if (isCarryingObject && isCollectTrigger)
                    {
                        objectToBring.SetActive(false);
                        anim.SetBool("isCarrying", false);
                        currentState = QuestState.Completed;
                        Debug.Log("Collect quest goal collected");
                    }
                    break;

                case QuestState.Completed:
                    dm.dialogueLines = dialogueLinesCompleted;
                    notifText.text = notifStartedText;
                    if ((dm.currentLine == dialogueLinesCompleted.Length - 1) && Input.GetButtonDown("Triangle"))
                    {
                        Debug.Log("Collect quest complete");
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
            dm.dialogueIcon = dm.defaultIcon;
        }

        if (inTrigger && Input.GetButtonDown("Triangle"))
        {
            Carry();

        }

        //Drop if isCarrying and "Circle" is pressed
        if (isCarryingObject && Input.GetButtonDown("Circle"))
        {
            Drop();

        }
    }

    private void Carry()
    {
        isCarryingObject = true;
        carrySlotFull = true;
        anim.SetBool("isCarrying", true);

        //Make weapon a child of the camera and move it to default position
        objectToBring.transform.SetParent(carryPos);
        objectToBring.transform.localPosition = Vector3.zero;
        objectToBring.transform.localRotation = Quaternion.Euler(Vector3.zero);
        objectToBring.transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //UI
        if (isCarryingObject)
        {
            cancelButton.SetActive(true);
            //cancelTMP.text = cancelText;
            actionButton.SetActive(false);
        }



    }

    private void Drop()
    {
        isCarryingObject = false;
        carrySlotFull = false;
        anim.SetBool("isCarrying", false);

        //Set parent to null
        objectToBring.transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        //rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        //UI
        if (!isCarryingObject)
        {
            cancelButton.SetActive(false);
            actionButton.SetActive(false);
        }



    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CarryObject"))
        {
            actionButton.SetActive(true);
            cancelButton.SetActive(false);
            //actionTMP.text = actionText;
            inTrigger = true;
            if (isCarryingObject)
            {
                actionButton.SetActive(false);
                cancelButton.SetActive(true);
                //cancelTMP.text = cancelText;
            }
        }


        if (other.CompareTag("CollectQuest"))
        {
            isCollectTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CarryObject"))
        {
            actionButton.SetActive(false);

            inTrigger = false;
        }

        if (other.CompareTag("CollectQuest"))
        {
            isCollectTrigger = false;
        }
    }
}
