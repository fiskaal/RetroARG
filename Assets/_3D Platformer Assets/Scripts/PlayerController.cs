using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    public Vector3 checkpoint;

    public float moveSpeed;
    public float jumpForce, gravityScale;
    private float yStore;

    public GameObject player;

    public CharacterController charCon;

    [SerializeField] private CameraController cam;

    private Vector3 moveAmount;

    public float rotateSpeed = 10f;

    public Animator anim;

    //public GameObject jumpParticle, landingParticle;
    private bool lastGrounded;
    public int jumpCount = 0;
    public int maxJumps = 3;

    public float bounceForce;

    [HideInInspector]
    public bool stopMoving;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();

        lastGrounded = true;
        //transform.position = checkpoint.position;

        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
        maxJumps = 3;
    }

    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            moveAmount.y = moveAmount.y + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);
        }
        else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0f )
        {

            yStore = moveAmount.y;

            moveAmount = (cam.transform.forward * Input.GetAxis("Vertical")) + (cam.transform.right * Input.GetAxis("Horizontal"));
            moveAmount.y = 0f;
            moveAmount = moveAmount.normalized;

            if (moveAmount.magnitude > .1f)
            {
                if (moveAmount != Vector3.zero)
                {
                    Quaternion newRot = Quaternion.LookRotation(moveAmount);

                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
                }
            }

            moveAmount.y = yStore;

            if (charCon.isGrounded)
            {
                //jumpParticle.SetActive(false);
                jumpCount = 0;

                if (!lastGrounded)
                {
                    
                    //landingParticle.SetActive(true);
                }

                if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
                {
                    moveAmount.y = jumpForce;
                    jumpCount++;

                    //jumpParticle.SetActive(true);

                    //AudioManager.instance.PlaySFXPitched(11);
                }else if (Input.GetButtonDown("Attack"))
                {
                    anim.SetTrigger("isAttacking");
                }
            }

            lastGrounded = charCon.isGrounded;

            charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);


            float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;

            anim.SetFloat("speed", moveVel);
            anim.SetBool("isGrounded", charCon.isGrounded);
            anim.SetFloat("yVel", moveAmount.y);
        }
    }

    public void Bounce()
    {
        moveAmount.y = bounceForce;

        charCon.Move(Vector3.up * bounceForce * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //transform.position = checkpoint.position;
            charCon.Move(new Vector3(checkpoint.x, checkpoint.y, checkpoint.z));
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //transform.position = checkpoint.position;
        }
    }
}
