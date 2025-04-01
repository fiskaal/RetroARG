using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard2 : MonoBehaviour
{
    public Transform target; // Assign the player's transform in the inspector

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Billboard: No target assigned!");
            return;
        }

        // Make the billboard always face the target completely
        transform.LookAt(target);
    }
}
