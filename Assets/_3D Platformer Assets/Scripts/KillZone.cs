using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] public Vector3 checkpoint;
    public Transform spawnpoint;
    [SerializeField] Animator animatorImage;
    public float respawnTime = 2f;
    private void Update()
    {
        checkpoint = spawnpoint.position;
    }
    //public Transform checkpoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animatorImage.PlayInFixedTime("ImageFadeInOut", -1, 0f);
            other.gameObject.GetComponent<CharacterController>().Move(checkpoint - other.transform.position);
            StartCoroutine(PauseCoroutine());
        }
    }

    private IEnumerator PauseCoroutine()
    {
        Time.timeScale = 0f; // Pause the game
        yield return new WaitForSecondsRealtime(respawnTime); // Wait for 2 seconds in real-time
        Time.timeScale = 1f; // Resume the game
    }
}
