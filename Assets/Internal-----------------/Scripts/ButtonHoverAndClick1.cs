using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHoverAndClick1 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverSound);


    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);


    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }



}
