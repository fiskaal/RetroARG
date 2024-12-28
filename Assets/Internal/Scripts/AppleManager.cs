using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering.LookDev;

public class AppleManager : MonoBehaviour
{
    [SerializeField] private TMP_Text appleText;
    [SerializeField] private int appleCount;
    [SerializeField] private int maxAppleCount = 3;
    [SerializeField] private AudioSource appleSound;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject portal;


    private void Awake()
    {
        portal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        appleText.text = appleCount.ToString() + ("/3");

        if (appleCount == maxAppleCount) 
        { 
        portal.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            other.gameObject.SetActive(false);
            appleCount++;
            //ps.Play();
            appleSound.Play();
        }
    }
    
}
