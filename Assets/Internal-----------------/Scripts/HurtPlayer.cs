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


    [Tooltip("Time to start teleporting the player.")]
    public float StartTeleport;

    [Tooltip("The player wait this time to can control the player again.")]
    public float TimeToControlPlayer;

    [Tooltip("Effect to start teleport.")] public GameObject TeleportEffect;

    public Transform TeleportPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TeleportPlayer(Transform player)
    {
        yield return new WaitForSeconds(StartTeleport);
        if (TeleportEffect)
        {
            Instantiate(TeleportEffect, player.position, player.rotation);
        }

        player.position = TeleportPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
        }
    }

    public void DamagePlayer()
    {
        animatorHurt.Play("NotificationAnimation");
        hm2.currentHealth -= dealDamage;
        Debug.Log("HURT!");
        animatorHurt.PlayInFixedTime("NotificationAnimation", -1, 0f);
        animatorImage.PlayInFixedTime("ImageFadeInOut", -1, 0f);

        StartCoroutine(GetComponent<MovementCharacterController>()
                .DeactivatePlayerControlByTime(TimeToControlPlayer));
        StartCoroutine(TeleportPlayer(transform));
    }
}
