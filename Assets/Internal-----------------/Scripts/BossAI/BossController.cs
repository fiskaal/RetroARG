using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Quest;

public class BossController : MonoBehaviour
{
    public enum BossState
    {
        Idle, Attacking, Dead
    }

    [Header("Boss States")]
    [SerializeField] private BossState currentState;

    [Header("Boss Info")]
    [SerializeField] private int health;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
