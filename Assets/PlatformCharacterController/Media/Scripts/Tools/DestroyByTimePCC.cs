using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimePCC : MonoBehaviour
{
    public float TimeToDestroy = 5;

    // Use this for initialization
    void Start()
    {
        Invoke("DestroyEffect", TimeToDestroy);
    }

    void DestroyEffect()
    {
        Destroy(gameObject);
    }
}