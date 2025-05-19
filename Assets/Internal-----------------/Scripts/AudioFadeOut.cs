using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime;

   

    /// <summary>
    /// Call this method to fade out the music over a duration in seconds.
    /// </summary>
    /// <param name="fadeDuration">Duration of the fade out in seconds.</param>
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine(fadeOutTime));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }

}


