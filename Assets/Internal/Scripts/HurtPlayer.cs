using System.Collections;
using System.Collections.Generic;
using TopDownShooter;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int dealDamage = 1;
    [SerializeField] public HM2 hm2;
    [SerializeField] Animator animator;
    
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
            animator.Play("NotificationAnimation");
            hm2.currentHealth -= dealDamage;
            Debug.Log("HURT!");
            animator.PlayInFixedTime("NotificationAnimation", -1, 0f);
        }
    }
}
