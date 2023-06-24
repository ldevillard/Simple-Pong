using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    public List<AudioClip> audioClips;

    public AudioClip GetClip(byte idx)
    {
        if (idx >= audioClips.Count)
        {
            Debug.LogError("AudioData: Index out of range");
            return null;
        }
        return audioClips[idx];
    }
}