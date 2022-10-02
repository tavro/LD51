using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip[] audioClips;

    [Range(0f, 1f)]
    public float volume;

    public bool isLoop;

    [HideInInspector]
    public AudioSource audioSource;
}
