using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    enum AIState
    {
        Idle, Patrolling, Chasing, Attacking
    }
    [Header("Patrol")]
    [SerializeField] private Animator enemyAnim;
    [SerializeField] private Transform waypoints;
    [SerializeField] private float waitAtPoint = 2f;
    [SerializeField] private Transform enemyChase;
    [SerializeField] private Transform enemyAttack;

    private int currentWaypoint;
    private float waitCounter;

    [Header("Components")]
    public NavMeshAgent agent;

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
    [Header("Player")]
    [SerializeField] private GameObject player;

    private void Start()
    {
        waitCounter = waitAtPoint;
        timeSinceLastSawplayer = susTime;
        timeToAttck = attackTime;
    }

    private void Update()
    {
        float distanceToplayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case AIState.Idle:
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    enemyAnim.Play("Bear_Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Bear_Walk", .2f);
                }

                if (distanceToplayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Bear_Walk", .2f);
                }
                break;

            case AIState.Patrolling:
                if (agent.remainingDistance <= .2f)
                {
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Bear_Walk", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                    enemyAnim.Play("Bear_Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                if (distanceToplayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    //enemyAnim.Play("Bear_Walk");
                    enemyAnim.CrossFadeInFixedTime("Bear_Walk", .2f);
                }
                break;
            case AIState.Chasing:
                agent.SetDestination(player.transform.position);
                if (distanceToplayer > chaseRange)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    timeSinceLastSawplayer -= Time.deltaTime;
                    enemyAnim.Play("Bear_Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);

                    if (timeSinceLastSawplayer <= 0)
                    {
                        currentState = AIState.Idle;
                        timeSinceLastSawplayer = susTime;
                        agent.isStopped = false;
                        enemyAnim.Play("Bear_Idle");
                        //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                    }
                }

                if (distanceToplayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    //enemyAnim.CrossFadeInFixedTime("Bear_Strike1", .1f);
                }

                break;
            case AIState.Attacking:
                //enemyAnim.CrossFadeInFixedTime("Bear_Strike1", .1f);
                transform.LookAt(player.transform.position, Vector3.up);
                timeToAttck -= Time.deltaTime;

                if (timeToAttck <= 0)
                {
                    enemyAnim.CrossFadeInFixedTime("Bear_Strike1", .1f);
                    //enemyAnim.Play("Bear_Attack1");
                    timeToAttck = attackTime;
                }

                if (distanceToplayer > attackRange)
                {
                    currentState = AIState.Chasing;
                    agent.isStopped = false;
                    enemyAnim.CrossFadeInFixedTime("Bear_Walk", .1f);
                    //enemyAnim.Play("Bear_walk");
                }
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
}
