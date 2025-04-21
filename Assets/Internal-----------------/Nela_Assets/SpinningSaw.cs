using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSaw : MonoBehaviour
{
    public float rotationSpeed = 30f;       // degrees per second
    public bool useLocalRotation = true;    // toggle for local or world space rotation

    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (useLocalRotation)
        {
            transform.Rotate(rotationAmount, 0f, 0f, Space.Self);
        }
        else
        {
            transform.Rotate(rotationAmount, 0f, 0f, Space.World);
        }
    }
}