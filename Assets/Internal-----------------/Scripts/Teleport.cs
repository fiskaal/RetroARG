using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketra
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] public Vector3 checkpoint;
        public Transform spawnpoint;
        public GameObject player;
        [SerializeField] Animator animatorImage;
        public float respawnTime = 2f;
        public bool inTrigger;
        public GameObject actionButton;
        public PlayerMovement pm;

        private void Start()
        {
            actionButton.SetActive(false);
            inTrigger = false;
        }
        private void Update()
        {
            checkpoint = spawnpoint.position;


        }
        //public Transform checkpoint;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inTrigger = true;
                actionButton.SetActive(true);

                if (inTrigger)
                {

                    if (Input.GetButtonDown("Square"))
                    {

                        animatorImage.PlayInFixedTime("ImageFadeInOut", -1, 0f);
                        //other.gameObject.GetComponent<CharacterController>().Move(checkpoint - other.transform.position);
                        StartCoroutine(PauseCoroutine());
                        
                        
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inTrigger = false;
                actionButton.SetActive(false);
            }
        }

        private IEnumerator PauseCoroutine()
        {
            //Time.timeScale = 0f; // Pause the game
            pm.isControlable = false;
            yield return new WaitForSecondsRealtime(respawnTime); // Wait for 2 seconds in real-time
            //Time.timeScale = 1f; // Resume the game
            player.transform.position = spawnpoint.position;
            pm.isControlable = true;
        }
    }
}
