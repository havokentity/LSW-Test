using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource cashRegisterSound, tweepSound, dangerSound, pleepSound, collectSound;

    public static SoundController instance;
    // Start is called before the first frame update
    void Start()
    {
        SoundController.instance = this;
    }

    public void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
