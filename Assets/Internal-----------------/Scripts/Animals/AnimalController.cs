using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    enum AnimalState
    {
        Idle, Patrolling, Running
    }
    [Header("Patrol")]
    [SerializeField] private Animator chickenAnim;
    [SerializeField] private Transform waypoints;
    [SerializeField] private float waitAtPoint = 10f;
    [SerializeField] private Transform animalChase;
    [SerializeField] private Transform animalAttack;

    private int currentWaypoint;
    private float waitCounter;

    [Header("Components")]
    public NavMeshAgent agent;

    [Header("AI States")]
    [SerializeField] private AnimalState currentState;

    [Header("Run Away")]
    [SerializeField] private float runAwayRange;

    [Header("Running")]
    [SerializeField] private float runRange = 2f;
    [SerializeField] private float runTime = 2f;
    private float timeToRun;

    //[Header("Sus")]
    //[SerializeField] public float susTime = 15f;
    //private float timeSinceLastSawPlayer;
    [Header("Player")]
    [SerializeField] private GameObject player;

    private void Start()
    {
        waitCounter = waitAtPoint;
        //timeSinceLastSawPlayer = susTime;
        timeToRun = runTime;
        runAwayRange = runRange;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case AnimalState.Idle:
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    chickenAnim.Play("Chicken_idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                else
                {
                    currentState = AnimalState.Patrolling;
                    agent.SetDestination(waypoints.GetChild(currentWaypoint).position);
                    //enemyAnim.Play("Bear_Walk");
                    chickenAnim.CrossFadeInFixedTime("Chicken_walk", .2f);
                }

                
                break;

            case AnimalState.Patrolling:
                if (agent.remainingDistance <= .1f)
                {
                    //enemyAnim.Play("Bear_Walk");
                    chickenAnim.CrossFadeInFixedTime("Chicken_walk", .2f);
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AnimalState.Idle;
                    waitCounter = waitAtPoint;
                    chickenAnim.Play("Chicken_idle");
                    //enemyAnim.CrossFadeInFixedTime("Bear_Idle", .2f);
                }
                //if (distanceToPlayer <= runAwayRange)
                //{
                //    currentState = AnimalState.Running;
                //    //enemyAnim.Play("Bear_Walk");
                //    chickenAnim.CrossFadeInFixedTime("Chicken_run", .2f);
                //}

                break;

            case AnimalState.Running:
                //currentWaypoint++;
                //if (currentWaypoint >= waypoints.childCount)
                //{
                //    currentWaypoint = 0;
                //}

                //if (distanceToPlayer > runAwayRange)
                //{
                //    currentState = AnimalState.Patrolling;
                //    //enemyAnim.Play("Bear_Walk");
                //    chickenAnim.CrossFadeInFixedTime("Chicken_run", .2f);
                //}
                break;



        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(animalAttack.position, runRange);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(enemyChase.position, chaseRange);


    }
}
