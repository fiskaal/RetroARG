using UnityEngine;

namespace KetraScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public float maxSpeed;
        public float rotSpeed;
        public float jumpSpeed;
        public float ySpeed;
        public float jumpButtonPressedPeriod;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        private float originalStepOffset;
        private float? lastGroundedTime;
        private float? jumpButtonPressedTime;
        public int jumpCount = 0;
        public int maxJumps = 2;

        // Start is called before the first frame update
        void Start()
        {
            originalStepOffset = characterController.stepOffset;
        }

        // Update is called once per frame
        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
            {
                inputMagnitude /= 2;
            }

            animator.SetFloat("InputMagnitude", inputMagnitude, .05f, Time.deltaTime);
            float speed = inputMagnitude * maxSpeed;
            movementDirection.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
                jumpCount = 0;
                ySpeed = -5f; // Small downward force to keep grounded
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
                if (jumpCount < maxJumps)
                {
                    ySpeed = jumpSpeed;
                    jumpCount++;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }

            characterController.stepOffset = characterController.isGrounded ? originalStepOffset : 0;

            Vector3 velocity = movementDirection * speed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                animator.SetBool("isMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
