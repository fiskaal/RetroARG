using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    public bool isTriggered;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            Debug.Log("IN_TRIGGER!");
        }
    }
    public void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = false;
            Debug.Log("OUT_OF_TRIGGER!");
        }
    }
}
