using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] AudioSource hit;
    [SerializeField] AudioSource realease;
    void HitSound()
    {
        hit.Play();
    }

    void ReleaseSound()
    {
        realease.Play();
    }
}
