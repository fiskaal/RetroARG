using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    [SerializeField] private Animator plateAnim;
    [SerializeField] private Animator platformAnim;
    
    [SerializeField] private AudioSource plateSound;
    public bool isPressed;
    public bool isSlid;
    [Header("Max 2 tags for now")]
    public string[] tags;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tags[0]) || other.gameObject.CompareTag(tags[1]))
        {
            plateSound.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tags[0]) || other.gameObject.CompareTag(tags[1]))
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
        if (other.gameObject.CompareTag(tags[0]) || other.gameObject.CompareTag(tags[1]))
        {
            isPressed = false;
            plateAnim.Play("PS_up");
            plateSound.Play();
            if (!isPressed)
            {
                isSlid=false;
                platformAnim.Play("SlideInMP");
               
            }

        }
    }
}
