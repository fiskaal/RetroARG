using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    enum AIState
    {
        Idle, Patrolling, Chasing, Attacking, Killed, Dead
    }
    [Header("Patrol")]
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform waypoints;
    [SerializeField] private float waitAtPoint = 2f;
    [SerializeField] private Transform enemyChase;
    [SerializeField] private Transform enemyAttack;

    private int currentWaypoint;
    private float waitCounter;

    [Header("Components")]
    public NavMeshAgent agent;
    public AudioSource bugKill;
    public PlayerAttack pa;

    [Header("AI States")]
    [SerializeField] private AIState currentState;

    [Header("Chasing")]
    [SerializeField] private float chaseRange;

    [Header("Attacking")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackTime = 2f;
    private float timeToAttck;

    [Header("Sus")]
    [SerializeField] public float susTime = 0f;
    private float timeSinceLastSawplayer;

    [Header("Dead")]
    public float dieTime = .5f;
    private float dieCounter;
    [SerializeField] private GameObject ps;
    [SerializeField] private Transform lootSpawnLocation;
    public List<LootItem> LootTable = new List<LootItem>();
    [Header("Player")]
    [SerializeField] private GameObject player;
    private Vector3 lookTarget;

    private void Start()
    {
        waitCounter = waitAtPoint;
        timeSinceLastSawplayer = susTime;
        timeToAttck = attackTime;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        lookTarget = player.transform.position;
        lookTarget.y = transform.position.y;

        switch (currentState)
        {
            case AIState.Idle:
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    enemyAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Walk", .2f);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Walk", .2f);
                }
                break;

            case AIState.Patrolling:
                if (agent.remainingDistance <= .2f)
                {
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Walk", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                    enemyAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Walk", .2f);
                }
                break;
            case AIState.Chasing:
                agent.SetDestination(player.transform.position);
                if (distanceToPlayer > chaseRange)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    timeSinceLastSawplayer -= Time.deltaTime;
                    enemyAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);

                    if (timeSinceLastSawplayer <= 0)
                    {
                        currentState = AIState.Idle;
                        timeSinceLastSawplayer = susTime;
                        agent.isStopped = false;
                        enemyAnim.Play("Idle");
                        //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                    }
                }

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    //enemyAnim.CrossFadeInFixedTime("Bear_Strike1", .1f);
                }

                break;

           
            case AIState.Attacking:
                //enemyAnim.CrossFadeInFixedTime("Bear_Strike1", .1f);
                transform.LookAt(lookTarget);
                //agent.SetDestination(player.transform.position, Vector3.zero);

                timeToAttck -= Time.deltaTime;

                if (timeToAttck <= 0)
                {
                    enemyAnim.CrossFadeInFixedTime("Attack", .1f);
                    //enemyAnim.Play("Bear_Attack1");
                    timeToAttck = attackTime;
                }

                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Chasing;
                    agent.isStopped = false;
                    enemyAnim.CrossFadeInFixedTime("Walk", .1f);
                    //enemyAnim.Play("Bear_walk");
                }
                break;
            case AIState.Killed:
                //bugKill.Play();
                enemyAnim.CrossFadeInFixedTime("Dead", .1f);

                break;

            case AIState.Dead:
                //enemyAnim.CrossFadeInFixedTime("Dead", .1f);
                break;


        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyAttack.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(enemyChase.position, chaseRange);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyKill();
        }
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, lootSpawnLocation.position, Quaternion.identity);
        }
    }

    public void EnemyKill()
    {
        enemy.SetActive(false);
        bugKill.Play();
        pa.enemiesKilled++;
        //dieCounter = dieTime;
        Instantiate(ps, transform.position, transform.rotation);
        currentState = AIState.Killed;
        foreach (LootItem lootItem in LootTable)
        {
            if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
        }
    }
}
