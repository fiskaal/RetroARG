using UnityEngine;
using UnityEngine.AI;

public class PigController : MonoBehaviour
{

    enum AIState
    {
        Idle, Walking, Escaping, Carried
    }

    [Header("Walking")]
    //[SerializeField] private Animator pigAnim;
    [SerializeField] private GameObject pig;
    [SerializeField] private Transform waypoints;
    [SerializeField] private float waitAtPoint = 2f;
    [SerializeField] private Transform pigChase;
    [SerializeField] private Transform pigEscape;

    private int currentWaypoint;
    private float waitCounter;

    [Header("Components")]
    public NavMeshAgent agent;
    public AudioSource pigIdle;
    public BringQuest bq;

    [Header("AI States")]
    [SerializeField] private AIState currentState;

    //[Header("Running")]
    //[SerializeField] private float runRange;

    [Header("Escaping")]
    [SerializeField] private float escapeRange = 3f;
    [SerializeField] private float escapeTime = 2f;
    private float timeToEscape;

    [Header("Sus")]
    [SerializeField] public float susTime = 0f;
    private float timeSinceLastSawplayer;

    [Header("Caried")]
    public float dieTime = .5f;
    private float dieCounter;
    [Header("Player")]
    [SerializeField] private GameObject player;
    private Vector3 lookTarget;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        //timeSinceLastSawplayer = susTime;
        timeToEscape = escapeTime;
    }

    // Update is called once per frame
    void Update()
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
                    //pigAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                else
                {
                    currentState = AIState.Walking;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                }

                if (distanceToPlayer <= escapeRange)
                {
                    currentState = AIState.Walking;
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                }
                break;

            case AIState.Walking:
                if (agent.remainingDistance <= .2f)
                {
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                    //pigAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                if (distanceToPlayer <= escapeRange)
                {
                    currentState = AIState.Escaping;
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                }
                break;
            case AIState.Escaping:
                agent.speed = 12;
                waitAtPoint = .1f;
                if (agent.remainingDistance <= .2f)
                {
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    
                    //pigAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                if (distanceToPlayer > escapeRange)
                {
                    currentState = AIState.Walking;
                    //enemyAnim.Play("Bear_Walk");
                    //pigAnim.CrossFadeInFixedTime("Walk", .2f);
                }

                break;
            
            case AIState.Carried:
                //pigAnim.CrossFadeInFixedTime("Dead", .1f);
                break;


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(pigEscape.position, escapeRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(enemyChase.position, chaseRange);


    }
}
