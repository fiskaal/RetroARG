using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVFX : MonoBehaviour
{

    [SerializeField] private GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(ps, transform.position, transform.rotation);
        }
    }
}
