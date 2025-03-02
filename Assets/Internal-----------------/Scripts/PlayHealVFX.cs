using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHealVFX : MonoBehaviour
{

    [SerializeField] private GameObject ps;
    [SerializeField] public HM2 hm2;
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
        if (other.gameObject.CompareTag("Player") && hm2.currentHealth < 5)
        {
            Instantiate(ps, transform.position, transform.rotation);
        }
    }
}
