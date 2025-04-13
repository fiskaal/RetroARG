using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField] private Animator plateAnim;
    [SerializeField] private Animator platformAnim;
    public bool isPressed;
    public bool isSlid;
    public string[] tag;
    public Tags tags;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tags.tags[1]))
        {
            isPressed = true;
            plateAnim.Play("PS_down");
            if (isPressed)
            {
                isSlid = true;
                platformAnim.Play("SlideOutMP");
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tag[1]))
        {
            isPressed = false;
            plateAnim.Play("PS_up");
            if (!isPressed)
            {
                isSlid=false;
                platformAnim.Play("SlideInMP");
            }

        }
    }
}
