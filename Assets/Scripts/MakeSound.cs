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
        var gm = FindObjectOfType<GameManager>();
        StartCoroutine(SoundPlayer(soundName, minWait, maxWait, gm));
    }

    public static IEnumerator SoundPlayer(string name, float minWait, float maxWait, GameManager gm)
    {
        for (; ; )
        {
            if(gm.CurrPauseState == GameManager.PauseState.NONE)
            {
                FindObjectOfType<AudioManager>().PlaySound(name);
            }
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }
    }
}
