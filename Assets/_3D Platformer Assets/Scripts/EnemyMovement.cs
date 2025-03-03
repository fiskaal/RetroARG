using UnityEngine;

namespace PlatformCharacterController
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed;
        public float turnSpeed;
        public Transform[] patrolPoints;
        private int currentPatrolPoint;
        public Rigidbody rb;
        private Vector3 moveDir;
        private Vector3 lookTarget;
        private float yPos;
        [SerializeField] private MovementCharacterController mcc;
        [SerializeField] public CustomTrigger ct;

        public enum EnemyState
        { idle, partolling, chasing, returning };

        public EnemyState currentState;

        public float waitTime = 1f;
        public float waitChance;
        private float waitCounter;



        public float chaseDistance;
        public float chaseSpeed;
        public float looseDistance;

        public float waitBeforeReturn;
        private float returnCounter;

        public float hopForce;
        public float waitToChase;
        private float chaseWaitCounter;

        private void Start()
        {
            foreach (Transform pp in patrolPoints)
            {
                pp.parent = null;
            }

            currentState = EnemyState.idle;
            waitCounter = waitTime;
        }

        private void Update()
        {

            switch (currentState)
            {
                case EnemyState.idle:

                    OnIdle();

                    break;
                case EnemyState.partolling:

                    OnPatrol();

                    break;
                case EnemyState.chasing:

                    OnChase();

                    break;
                case EnemyState.returning:

                    OnReturn();

                    break;
            }
            if (currentState != EnemyState.chasing)
            {
                if (Vector3.Distance(mcc.transform.position, transform.position) < chaseDistance)
                {
                    currentState = EnemyState.chasing;

                    rb.velocity = Vector3.up * hopForce;
                    chaseWaitCounter = waitToChase;
                }
            }
            lookTarget.y = transform.position.y;
            //transform.LookAt(lookTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime);
        }


        public void NextPatrolPoint()
        {
            if (Random.Range(0f, 100f) < waitChance)
            {
                waitCounter = waitTime;
                currentState = EnemyState.idle;
            }
            else
            {
                waitCounter = waitTime;

            }
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }
        }
        // Idle state
        public void OnIdle()
        {

            yPos = rb.velocity.y;
            rb.velocity = new Vector3(0f, yPos, 0f);

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                currentState = EnemyState.partolling;

                NextPatrolPoint();
            }

        }
        // Patrol state
        public void OnPatrol()
        {

            yPos = rb.velocity.y;
            moveDir = patrolPoints[currentPatrolPoint].position - transform.position;

            moveDir.y = 0f;
            moveDir.Normalize();
            rb.velocity = moveDir * moveSpeed;
            rb.velocity = new Vector3(rb.velocity.x, yPos, rb.velocity.z);

            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= .5f)
            {
                NextPatrolPoint();
            }
            else
            {
                lookTarget = patrolPoints[currentPatrolPoint].position;
            }

        }
        // Chase state
        public void OnChase()
        {
            lookTarget = mcc.transform.position;

            if (chaseWaitCounter > 0)
            {
                chaseWaitCounter -= Time.deltaTime;
            }
            else
            {

                yPos = rb.velocity.y;
                moveDir = mcc.transform.position - transform.position;

                moveDir.y = 0f;
                moveDir.Normalize();
                rb.velocity = moveDir * chaseSpeed;
                rb.velocity = new Vector3(rb.velocity.x, yPos, rb.velocity.z);
            }
            if (Vector3.Distance(mcc.transform.position, transform.position) > looseDistance )
            {
                currentState = EnemyState.returning;

                returnCounter = waitBeforeReturn;
            }



        }
        // Return state
        public void OnReturn()
        {

            currentState = EnemyState.partolling;
            returnCounter -= Time.deltaTime;
            if (returnCounter <= 0)
            {
                currentState = EnemyState.partolling;
            }


        }



    }
}
