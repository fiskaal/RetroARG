using PlatformCharacterController;
using System.Collections;
using System.Collections.Generic;
using TopDownShooter;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int dealDamage = 1;
    [SerializeField] public HM2 hm2;
    [SerializeField] Animator animatorHurt;
    [SerializeField] Animator animatorImage;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform checkpoint;

   
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer();
            player.transform.position = checkpoint.position;
        }
    }

    public void DamagePlayer()
    {
        animatorHurt.Play("NotificationAnimation");
        hm2.currentHealth -= dealDamage;
        Debug.Log("HURT!");
        animatorHurt.PlayInFixedTime("NotificationAnimation", -1, 0f);
        animatorImage.PlayInFixedTime("ImageFadeInOut", -1, 0f);
        player.transform.position = checkpoint.position;
        
    }
}
