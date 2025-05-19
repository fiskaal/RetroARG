using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cameras;
    
    public Transform target;
    // Assign the player's transform in the inspector
    private void Awake()
    {
        cameras = GameObject.Find("Cameras");
        target = cameras.transform.Find("Camera"); 
    }
   
    void Update()
    {
        
        if (target == null)
        {
            Debug.LogWarning("Billboard: No target assigned!");
            return;
        }

        // Get direction from this object to the target
        Vector3 direction = target.position - transform.position;

        // Keep only the horizontal direction (zero out Y-axis)
        direction.y = 0;

        // If the direction is not zero, apply rotation
        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}