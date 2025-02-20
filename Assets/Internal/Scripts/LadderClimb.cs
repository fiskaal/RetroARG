using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f;
    public float horizontalSpeed = 3f;
    //public KeyCode climbKey = KeyCode.W;
    //public KeyCode descendKey = KeyCode.S;

    private bool isClimbing = false;
    public CharacterController characterController;
    private Vector3 moveDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    private void Update()
    {
        if (isClimbing)
        {
            ClimbLadder();
        }
    }

    private void ClimbLadder()
    {
        float verticalInput = 0f;
        if (Input.GetButtonDown("Jump"))
        {
            verticalInput = 1f;
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            verticalInput = -1f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontalInput * horizontalSpeed, verticalInput * climbSpeed, 0);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
