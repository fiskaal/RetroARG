using UnityEngine;
using TMPro;


namespace Ketra
{
    public class CarryObjectController : MonoBehaviour
    {
        public Transform carryPos;
        //public PlayerMovement pm;
        public Animator animator;
        public bool isCarrying;
        public GameObject actionButton;
        public TMP_Text carryText;
        public GameObject cancelButton;
        public TMP_Text dropText;
        public GameObject weapon;
        public GameObject carryObject;
        public Rigidbody rb;
        public 


        // Start is called before the first frame update
        void Start()
        {
            actionButton.SetActive(false);
            cancelButton.SetActive(false);
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            //rb.constraints = RigidbodyConstraints.FreezePositionX;
            //rb.constraints = RigidbodyConstraints.FreezePositionZ;
            //rb.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Circle") && isCarrying)
            {
                isCarrying = false;
                //carryObject.gameObject.transform.position = transform.position;
                Debug.Log("NOT CARRYING!");
                carryObject.transform.SetParent(null);
                rb.useGravity = true;
                weapon.SetActive(true);
                animator.SetBool("isCarrying", false);
                cancelButton.SetActive(false);
                //rb.constraints = RigidbodyConstraints.FreezeRotation;
                //rb.constraints = RigidbodyConstraints.FreezeRotationY;
                //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                //rb.constraints = RigidbodyConstraints.FreezePositionX;
                //rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.freezeRotation = true;
                rb.isKinematic = false;
                
            }

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("CarryObject"))
            {
                actionButton.SetActive(true);
                carryText.text = "CARRY";
                if (Input.GetButton("Triangle") && !isCarrying)
                {
                    isCarrying = true;
                    rb.useGravity = false;
                    rb.isKinematic = true;

                    rb.freezeRotation = true;
                    //rb.constraints = RigidbodyConstraints.FreezeAll;
                    carryObject.transform.localPosition = carryPos.transform.position;
                    carryObject.transform.SetParent(carryPos);
                    carryObject.transform.localPosition = Vector3.zero;
                    carryObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    carryObject.transform.localScale = Vector3.one;
                    Debug.Log("IS CARRYING!");
                    weapon.SetActive(false);
                    animator.SetBool("isCarrying", true);
                    //pm.maxSpeed = moveSpeed;

                }
                else if (isCarrying)
                {
                    actionButton.SetActive(false);
                    cancelButton.SetActive(true);
                    dropText.text = "DROP";
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
