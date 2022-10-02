using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSound : MonoBehaviour
{
    [SerializeField]
    private string soundName;
    [SerializeField]
    private float minWait;
    [SerializeField]
    private float maxWait;

    void Start()
    {
        StartCoroutine(SoundPlayer(soundName, minWait, maxWait));
    }

    IEnumerator SoundPlayer(string name, float minWait, float maxWait)
    {
        for (; ; )
        {
            FindObjectOfType<AudioManager>().PlaySound(name);
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }
}
