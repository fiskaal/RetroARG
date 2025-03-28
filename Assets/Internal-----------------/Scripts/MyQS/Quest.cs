using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public enum QuestState
    {
        NotTaken, Active, Completed
    }
    [Header("Quest States")]
    [SerializeField] public QuestState currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ProcessDialogue()
    {
        Debug.Log("base process dialogue");
    }
}
