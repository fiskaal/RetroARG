using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector cutscene;
    public bool hasPlayed;
    public GameObject actionButton;
    public bool inTrigger;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger) 
        { 
            
            if (Input.GetButtonDown("Square"))
            {
                cutscene.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionButton.SetActive(true);
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            actionButton.SetActive(false);
            inTrigger = false;
        }
    }
}
