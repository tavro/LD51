using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    int buttonPress = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Press()
    {
        buttonPress++;
        Debug.Log("hej" + buttonPress);
    }
}
