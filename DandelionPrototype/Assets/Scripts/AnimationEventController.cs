using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    public Animator playerAnimator;
    //public ParticleSystem particles;
    public AudioSource audioSrc;
    public ParticleSystem particles;

    public void PlaySFX(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }

    public void PlayParticles()
    {
        particles.Play();
    }

    public void StopParticles()
    {
        particles.Stop();
    }
}
