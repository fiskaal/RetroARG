using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] AudioSource? hit;
    [SerializeField] AudioSource? realease;
    [SerializeField] AudioSource? die;
    [SerializeField] AudioSource? step;
    [SerializeField] AudioSource? step2;

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

    void StepSound1()
    {
        step.Play();
    }
    void StepSound2()
    {
        step2.Play();
    }
}
