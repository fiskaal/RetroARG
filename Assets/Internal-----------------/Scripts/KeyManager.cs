using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private int keyCount = 0;
    [SerializeField] private int maxKeyCount = 3;
    [SerializeField] private AudioSource keySound;
    //[SerializeField] private ParticleSystem ps;
    //[SerializeField] private GameObject portal;
    [SerializeField] private Animator animatorIcon;
    
    // Start is called before the first frame update
    void Awake()
    {
        //keyCount = PlayerPrefs.GetInt("Keys");
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCount == maxKeyCount)
        {
            
            
        }

        keyText.text = keyCount.ToString() + "/3";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            //other.gameObject.SetActive(false);
            other.transform.parent.gameObject.SetActive(false);
            keyCount++;
            animatorIcon.PlayInFixedTime("iconSizeUpDown", -1, 0f);
            //ps.Play();
            //Instantiate(ps, transform.position, transform.rotation);
            keySound.Play();
            //PlayerPrefs.SetInt("Keys", keyCount);


        }
    }
}
