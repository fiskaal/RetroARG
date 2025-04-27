using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] AudioSource? hit;
    [SerializeField] AudioSource? realease;
    [SerializeField] AudioSource? die;
    void HitSound()
    {
        hit.Play();
    }

    void ReleaseSound()
    {
        realease.Play();
    }

    void DieSound()
    {
        die.Play();
    }
}
