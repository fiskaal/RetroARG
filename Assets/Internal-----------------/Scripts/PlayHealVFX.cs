using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHealVFX : MonoBehaviour
{

    [SerializeField] private GameObject ps;
    [SerializeField] private HealthSystem hs;
    // Start is called before the first frame update
    void Awake()
    {
        hs = FindObjectOfType<HealthSystem>();
    }
    
    // void Start()
    // {
    //     hs = GetComponent<HealthSystem>();
    // }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hs.currentHealth < 5)
        {
            Instantiate(ps, transform.position, transform.rotation);
        }
    }
}
