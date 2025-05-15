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
    public bool isFollowing;
    public AudioSource goatSound;
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
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (isFollowing) { 
        switch (currentState)
        {
            case FollowerState.Idle:

                if (distanceToPlayer > followRange)
                {
                    currentState = FollowerState.Following;
                    agent.isStopped = false;
                    followerAnim.CrossFadeInFixedTime("Walk", .1f);
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
                    followerAnim.Play("Idle");
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                }


                break;
            case FollowerState.Following:
                agent.SetDestination(player.transform.position);
                goatSound.Play();

                if (distanceToPlayer <= idleRange)
                {
                    currentState = FollowerState.Idle;
                    followerAnim.CrossFadeInFixedTime("Idle", .1f);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                    
                }

                break;
        }
        }
        else if(!isFollowing)
        {
            currentState = FollowerState.Idle;
            followerAnim.Play("Idle");
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
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
