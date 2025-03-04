using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    public CharacterController charCon;

    //[SerializeField]private CameraController camCon;

    private Vector3 moveAmount;

    public float rotateSpeed = 10f;

    public Animator anim;

    public GameObject jumpParticle;
    public GameObject landingParticle;
    private bool lastGrounded;

    public float bounceForce;
    // Start is called before the first frame update
    void Start()
    {
        lastGrounded = true;

        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bounce()
    {
        moveAmount.y = bounceForce;

        charCon.Move(Vector3.up * bounceForce * Time.deltaTime);
    }
}
