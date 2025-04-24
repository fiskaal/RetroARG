using Unity.Mathematics;
using UnityEngine;

namespace Ketra
{
    public class PlayerMovement : MonoBehaviour
    {
        public float maxSpeed;
        public float rotSpeed;
        public float jumpHeight;
        public float gravityMultiplier;
        public float ySpeed;
        public float jumpButtonGracePeriod;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform cameraTransform;
        private float originalStepOffset;
        private float? lastGroundedTime;
        private float? jumpButtonPressedTime;
        public int jumpCount = 0;
        public int maxJumps = 2;
        public bool isJumping;
        public bool isGrounded;
        public bool isControlable;

        // Start is called before the first frame update
        void Start()
        {
            originalStepOffset = characterController.stepOffset;
        }

        // Update is called once per frame
        void Update()
        {
            if (isControlable)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
                float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

                //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                //{
                //    inputMagnitude /= 2;
                //}

                animator.SetFloat("InputMagnitude", inputMagnitude, .01f, Time.deltaTime);
                float speed = inputMagnitude * maxSpeed;
                movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
                movementDirection.Normalize();

                float gravity = Physics.gravity.y * gravityMultiplier;

                if (isJumping && ySpeed > 0 && Input.GetButton("Cross") == false)
                {
                    gravity *= 2;
                }

                ySpeed += gravity * Time.deltaTime;

                if (characterController.isGrounded)
                {
                    lastGroundedTime = Time.time;
                    jumpCount = 0;
                    ySpeed = -5f; // Small downward force to keep grounded
                    animator.SetBool("isGrounded", true);
                    isGrounded = true;
                    animator.SetBool("isJumping", false);
                    isJumping = false;
                    animator.SetBool("isFalling", false);
                }
                else
                {
                    characterController.stepOffset = 0;
                    animator.SetBool("isGrounded", false);
                    isGrounded = false;

                    if ((isJumping && ySpeed < 0) || ySpeed < -2)
                    {
                        animator.SetBool("isFalling", true);
                    }
                }

                if (Input.GetButtonDown("Cross"))
                {
                    jumpButtonPressedTime = Time.time;

                    if (jumpCount < maxJumps)
                    {
                        ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                        animator.SetBool("isJumping", true);
                        isJumping = true;
                        jumpCount++;
                        jumpButtonPressedTime = null;
                        lastGroundedTime = null;
                    }
                }
                if (Input.GetButtonDown("Circle") && isGrounded)
                {

                    animator.SetTrigger("Attacking");
                }


                characterController.stepOffset = characterController.isGrounded ? originalStepOffset : 0;

                Vector3 velocity = movementDirection * speed;
                velocity = AdjustVelocityToSlope(velocity);
                velocity.y += ySpeed;

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
            else
            {
                isControlable = false;
            }
        }

        private Vector3 AdjustVelocityToSlope(Vector3 velocity) 
        { 
        var ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f)) 
            { 
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                var adjustedVelocity = slopeRotation * velocity;

                if (adjustedVelocity.y < 0) 
                { 
                return adjustedVelocity;
                }
            }
            return velocity;
        }
    }
}
