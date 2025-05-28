using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

public class SceneLoader : MonoBehaviour
{
    public Image image;
    
    //public GameObject image2;
    [SerializeField] private Animator imageAnimator;
    public float waitTime = 1f;
    public string sceneName;
    public GameObject actionButton;
    public bool inTrigger = false;
    public KeyManager km;
    public GameObject notification;
    public TMP_Text notifText;
    public Animator notifAnim;
    public AudioSource alertSound;
    public AudioSource travelSound;
    public AudioFadeOut audioFadeOut;
    public HealthSystem hs;



    //public GameObject textInfo;
   
    

    public void Awake()
    {
        image.raycastTarget = false;
        
        //image2.gameObject.SetActive(false);
    }

    public void Update()
    {
        
        if (inTrigger)
        {
            notifText.text = km.keyCount + "/" + km.maxKeyCount + " keys found.";

            if (Input.GetButtonDown("Square") && km.keyCount >= km.maxKeyCount)
            {
                
                imageAnimator.Play("ImageFadeIn");
                travelSound.Play();
                StartCoroutine(WaitAndLoad(waitTime, sceneName));
                audioFadeOut.FadeOut();
            }else if (Input.GetButtonDown("Square") && km.keyCount < km.maxKeyCount)
            {
                notifAnim.PlayInFixedTime("NotificationAnimation", -1, 0f);
                alertSound.Play();
            }
           
        }
    }

    public void LoadGameScene(string sceneName)
    {
        Time.timeScale = 1;
        image.raycastTarget = true;
        imageAnimator.Play("ImageFadeIn");
        StartCoroutine(WaitAndLoad(waitTime, sceneName));
    }

    public void LoadGameSceneWithGamePaused(string sceneName)
    {
        Time.timeScale = 1;
        image.raycastTarget = true;
        //image2.gameObject.SetActive(true);
        imageAnimator.Play("ImageFadeIn", 0, .5f);
        StartCoroutine(WaitAndLoad(waitTime, sceneName));
    }

    public void LoadMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TestAnimation(string sceneName)
    {
        image.raycastTarget = true;
        imageAnimator.Play("ImageFadeIn");
        StartCoroutine(WaitAndLoad(waitTime, sceneName));

    }

    public void FadeInImage()
    {
        image.raycastTarget = true;
    }

    public void FadeOutImage()
    {

    }

    IEnumerator WaitAndLoad(float waitTime, string sceneName)
    {
        yield return new WaitForSeconds(waitTime);
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadSceneAsync(sceneName);
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        Time.timeScale = 1;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            actionButton.SetActive(true);
            //textInfo.SetActive(true);
            PlayerPrefs.SetInt("Hearts", hs.currentHealth);
            PlayerPrefs.Save();
            inTrigger = true;
            
        }

        

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            actionButton.SetActive(false);
            //textInfo.SetActive(true);
            inTrigger = false;

        }
    }
}
