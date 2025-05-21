using UnityEngine;
using UnityEngine.AI;

public class PigController : MonoBehaviour
{

    public enum AIState
    {
        Idle, Walking, Escaping, Carried
    }

    [Header("Walking")]
    [SerializeField] private Animator pigAnim;
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
    [SerializeField] public AIState currentState;

    //[Header("Running")]
    //[SerializeField] private float runRange;

    //[Header("Escaping")]
    //[SerializeField] private float escapeRange = 3f;
    //[SerializeField] private float escapeTime = 2f;
    //private float timeToEscape;

    [Header("Sus")]
    [SerializeField] public float susTime = 0f;
    private float timeSinceLastSawplayer;

    [Header("Caried")]
    public float dieTime = .5f;
    private float dieCounter;
    [SerializeField] private bool isCarried;
    [SerializeField] private float carryRange;
    public bool canCarry;
    [Header("Player")]
    [SerializeField] private GameObject player;
    private Vector3 lookTarget;
    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        //timeSinceLastSawplayer = susTime;
        //timeToEscape = escapeTime;
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
                    pigAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                else
                {
                    currentState = AIState.Walking;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                    //enemyAnim.Play("Bear_Walk");
                    pigAnim.CrossFadeInFixedTime("Run", .2f);
                }

                //if (distanceToPlayer <= carryRange)
                //{
                //    canCarry = true;
                //    if(canCarry && Input.GetButtonDown("Triangle"))
                //    {
                //        currentState = AIState.Carried;
                //    }
                //}
                break;

            case AIState.Walking:
                if (agent.remainingDistance <= .2f)
                {
                    
                    pigAnim.CrossFadeInFixedTime("Run", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                    pigAnim.Play("Idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                //if (distanceToPlayer <= carryRange)
                //{
                //    canCarry = true;
                //    if (canCarry && Input.GetButtonDown("Triangle"))
                //    {
                //        isCarried = true;
                //        currentState = AIState.Carried;
                //    }
                //}
                break;

            case AIState.Carried:
                pigAnim.SetBool("isIdle", true);
                //if (Input.GetButtonDown("Triangle"))
                //{
                //    isCarried = false;
                //}
                break;


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(pigEscape.position, carryRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(enemyChase.position, chaseRange);


    }

    public void Carry()
    {

    }
}
