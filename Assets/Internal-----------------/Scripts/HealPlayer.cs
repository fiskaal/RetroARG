using PlatformCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] public HM2 hm2;
    public int healAmount = 1;
    [SerializeField] public Animator animatorHeal;
    [SerializeField] public Animator animatorIcon;
    [SerializeField] private AudioSource healSound;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal") && hm2.currentHealth < 5)
        {
            
            hm2.currentHealth += healAmount;
            Debug.Log("HEAL!");
            animatorHeal.PlayInFixedTime("NotificationAnimation", -1, 0f);
            animatorIcon.PlayInFixedTime("iconSizeUpDown", -1, 0f);
            //other.gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);
            healSound.Play();

        }
    }
    

}
