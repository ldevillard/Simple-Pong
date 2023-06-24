using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager Instance;

    public AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    public AudioData buttonAudioData;

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonAudioData.GetClip(0));
    }

    public void PlaySound(AudioClip clip, bool pitch = false)
    {
        if (pitch)
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }
}
