using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVFX : MonoBehaviour
{
    public HealthSystem hs;
    [SerializeField] private GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hs.currentHealth < hs.maxHealth)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(ps, transform.position, transform.rotation);
            }
        }
    }
}
