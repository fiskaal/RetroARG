using EasyUIAnimator;
using TMPro;
using UnityEngine;

public class PickUpController : MonoBehaviour
{

    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, carryPos, fpsCam;
    public GameObject carryObject;
    public Animator anim;
    public GameObject actionButton;
    public TMP_Text actionTMP;
    public GameObject cancelButton;
    public TMP_Text cancelTMP;
    public string actionText, cancelText;


    public float dropForwardForce, dropUpwardForce;

    public bool isCarrying;
    public static bool slotFull;
    public bool inTrigger = false;

    private void Start()
    {
        //Setup
        if (!isCarrying)
        {

            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (isCarrying)
        {

            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        
        
        if (inTrigger && Input.GetButtonDown("Triangle"))
        {
            Carry();
            
        }

        //Drop if isCarrying and "Circle" is pressed
        if (isCarrying && Input.GetButtonDown("Circle"))
        {
            Drop();
            
        }
    }

    private void Carry()
    {
        isCarrying = true;
        slotFull = true;
        anim.SetBool("isCarrying", true);

        //Make weapon a child of the camera and move it to default position
        carryObject.transform.SetParent(carryPos);
        carryObject.transform.localPosition = Vector3.zero;
        carryObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        carryObject.transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //UI
        if (isCarrying)
        {
            cancelButton.SetActive(true);
            cancelTMP.text = cancelText;
            actionButton.SetActive(false);
        }
        


    }

    private void Drop()
    {
        isCarrying = false;
        slotFull = false;
        anim.SetBool("isCarrying", false);

        //Set parent to null
        carryObject.transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        //UI
        if (!isCarrying)
        {
            cancelButton.SetActive(false);
            actionButton.SetActive(false);
        }
        


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CarryObject"))
        {
            actionButton.SetActive(true);
            cancelButton.SetActive(false);
            actionTMP.text = actionText;
            inTrigger = true;
            if (isCarrying) 
            {
                actionButton.SetActive(false);
                cancelButton.SetActive(true);
                cancelTMP.text = cancelText;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CarryObject"))
        {
            actionButton.SetActive(false);
            
            inTrigger = false;
        }
    }
}
