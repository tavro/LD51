using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;


    void Awake()
    {
        foreach(var sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.isLoop;
            if(sound.name.ToLower() == "theme")
            {
                sound.audioSource.clip = sound.audioClips[0];
                sound.audioSource.Play();
            }
        }
    }

    public void PlaySound(string name)
    {
        var sound = Array.Find(sounds, sound => sound.name == name);
        try
        {
            sound.audioSource.clip = sound.audioClips[Random.Range(0,sound.audioClips.Length-1)];
            sound.audioSource.Play();
        }
        catch(Exception err)
        {
            Debug.LogError("Tried to play sound " + name + " but something went wrong :C");
            Debug.LogException(err);
        }
    }
}
