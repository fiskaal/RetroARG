using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformCharacterController { 
    public class LadderClimb : MonoBehaviour

    {
        private float _vertical;
        private float speed = 5f;
        private bool isLadder;
        private bool isClimbing = false;
        [SerializeField] private Animator playerAnim;
        //private float _vertical;

        [SerializeField] private MovementCharacterController mcc;
        [SerializeField] private PlayerInput PlayerInputs;



    // Update is called once per frame
        void Update()
        {
            _vertical = PlayerInputs.GetVertical();
            //_vertical = Input.GetAxis("Vertical");

            if (isLadder && Mathf.Abs(_vertical) > 0f)
            {
                isClimbing = true;
            }
        }

        private void FixedUpdate()
        {
            if (isClimbing)
            {
                mcc.Gravity = 0f;
                //playerAnim.SetTrigger("Climb");
                mcc._velocity = new Vector3(mcc._velocity.x, _vertical * speed);
            }
            else
            {
                mcc.Gravity = -30f;
            }
        }

        private void SetClimbingState(bool climbing)
        {
            mcc.Climbing = climbing;
            mcc.PlayerAnimator.SetTrigger("Climb");
            mcc.PlayerAnimator.SetBool("Climbing", climbing);
        }

        private void OnTriggerEnter(Collider collision)
        {
            //if (collision.CompareTag("Ladder"))
            //{
            //    isLadder = true;
            //    isClimbing = true;
            //    SetClimbingState(true);
            //    Debug.Log("CLIMBING!");
            //}

        }

        private void OnTriggerExit(Collider collision)
        {
            //if (collision.CompareTag("Ladder"))
            //{
            //    Debug.Log("NOT CLIMBING!");
            //    SetClimbingState(false);
            //    isLadder = false;
            //    isClimbing = false;

            //}
        }
    }
}