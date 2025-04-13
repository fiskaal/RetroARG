using UnityEngine;

namespace Ketra
{
    public class CarryObjectController : MonoBehaviour
    {
        public Transform carryPos;
        public PlayerMovement pm;
        public Animator animator;
        public bool isCarrying;
        public GameObject actionButton;
        public GameObject cancelButton;
        public GameObject weapon;
        public GameObject carryObject;
        public Rigidbody rb;


        // Start is called before the first frame update
        void Start()
        {
            actionButton.SetActive(false);
            cancelButton.SetActive(false);
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel") && isCarrying)
            {
                isCarrying = false;
                //carryObject.gameObject.transform.position = transform.position;
                Debug.Log("NOT CARRYING!");
                carryObject.transform.SetParent(null);
                rb.useGravity = true;
                weapon.SetActive(true);
                animator.SetBool("isCarrying", false);
                cancelButton.SetActive(false);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                //rb.constraints = RigidbodyConstraints.FreezeRotationY;
                //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.freezeRotation = true;
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("CarryObject"))
            {
                actionButton.SetActive(true);
                if (Input.GetButton("Interact") && !isCarrying)
                {
                    isCarrying = true;
                    rb.useGravity = false;
                    

                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    carryObject.transform.position = carryPos.transform.position;
                    carryObject.transform.SetParent(carryPos.transform);
                    Debug.Log("IS CARRYING!");
                    weapon.SetActive(false);
                    animator.SetBool("isCarrying", true);
                    //pm.maxSpeed = moveSpeed;

                }
                else if (isCarrying)
                {
                    actionButton.SetActive(false);
                    cancelButton.SetActive(true);
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("CarryObject"))
            {
                actionButton.SetActive(false);
            }
        }
    }
}
