using UnityEngine;
using UnityEngine.AI;

public class FollowerController : MonoBehaviour
{
    enum FollowerState
    {
        Idle, Following, Running
    }

    [Header("Follower")]
    [SerializeField] private Animator followerAnim;
    [SerializeField] private Transform followerChase;
    [SerializeField] private Transform followerIdle;
    //[SerializeField] private Transform animalAttack;

    [Header("Components")]
    public NavMeshAgent agent;

    [Header("Follower States")]
    [SerializeField] private FollowerState currentState;

    [Header("Idle")]
    [SerializeField] private float idleRange = 2f;
    [SerializeField] private float idleTime = 2f;

    [Header("Following")]
    [SerializeField] private float followRange = 2f;
    [SerializeField] private float followTime = 2f;


    [Header("Player")]
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case FollowerState.Idle:

                if (distanceToPlayer > followRange)
                {
                    currentState = FollowerState.Following;
                    agent.isStopped = false;
                    followerAnim.CrossFadeInFixedTime("Dog_run", .5f);
                    //enemyAnim.Play("Bear_walk");
                }

                //if (distanceToPlayer <= followRange)
                //{
                //    currentState = FollowerState.Following;
                //    //enemyAnim.Play("Bear_Walk");
                //    followerAnim.CrossFadeInFixedTime("Dog_walk", .2f);
                //}
                else
                {
                    currentState = FollowerState.Idle;
                    followerAnim.Play("Dog_idle");
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                }


                break;
            case FollowerState.Following:
                agent.SetDestination(player.transform.position);
                

                if (distanceToPlayer <= idleRange)
                {
                    currentState = FollowerState.Idle;
                    followerAnim.CrossFadeInFixedTime("Dog_idle", .5f);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    
                }

                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(followerIdle.position, idleRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(followerChase.position, followRange);


    }
}
