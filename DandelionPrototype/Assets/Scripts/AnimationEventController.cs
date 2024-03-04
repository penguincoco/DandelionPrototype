using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    public Animator playerAnimator;
    //public ParticleSystem particles;
    public AudioSource audioSrc;

    public void PlaySFX(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
