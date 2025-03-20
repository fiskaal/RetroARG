using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int questNumber;
    [SerializeField] private QuestManager qm;
    public string itemName;
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (!qm.questCompleted[questNumber] && qm.quests[questNumber].gameObject.activeSelf)
            {
                qm.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}
