using UnityEngine;
using Cinemachine;
using System;
using Utilities;
using System.Collections.Generic;
using static Utilities.Timer;

namespace Platformer
{

    public class PlayerController : MonoBehaviour 
    { 
    
        [Header("References")]
        //[SerializeField] CharacterController charController;
        [SerializeField] Rigidbody rb;
        [SerializeField] Animator animator;
        [SerializeField] GroundChecker groundChecker;
        [SerializeField] CinemachineFreeLook freeLookCam;
        [SerializeField] InputReader inputReader;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotateSpeed = 15f;
        [SerializeField] float smoothTime = .2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = .5f;
        [SerializeField] float jumpCooldown = 5f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;

        Transform mainCam;

        const float ZeroF = 0f;

        public float currentSpeed;
        public float velocity;
        public float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        // animator parameters

        static readonly int speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookCam.Follow = transform;
            freeLookCam.LookAt = transform;
            freeLookCam.OnTargetObjectWarped(transform, positionDelta: transform.position - freeLookCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;

            //timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(2) { jumpTimer, jumpCooldownTimer };

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

        }
        private void Start()
        {
            inputReader.EnablePlayerActions();
        }
        private void Update()
        {
            movement = new Vector3(inputReader.Direction.x, 0f, inputReader.Direction.y);
           
            HandleAnimator();

        }

        private void FixedUpdate()
        {
            //HandleJump();
            HandleMovement();
        }




        private void HandleAnimator()
        {
            animator.SetFloat(speed, currentSpeed);

        }

        private void HandleMovement()
        {
            // var movementDirection = new Vector3(inputReader.Direction.x, 0f, inputReader.Direction.y).normalized;
            // rotate movement dir to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;

            if(adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);

                HandleHorizontalMovement(adjustedDirection);

                SmoothSpeed(adjustedDirection.magnitude);

            }
            else
            {
                SmoothSpeed(ZeroF);

                //reset horizontal velocity 
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);

            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            //move player
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            //adjust rotation to match movement direction
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed=  Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }

}
