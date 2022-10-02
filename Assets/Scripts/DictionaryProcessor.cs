using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryProcessor
{
    public static string PATH = Application.dataPath + "/Text/dictitonary.txt";

    public static string GetWordOfLength(int length) {
        string[] lines = System.IO.File.ReadAllLines(PATH);
        List<string> result = new List<string>();

        foreach(string line in lines) {
            if(line.Length == length) {
                result.Add(line);
            }
        }
        
        Debug.Log(result.Count);
        return result[Random.Range(0, result.Count)];
    }

    public static KeyCode StringToKeyCode(string letter) {
        KeyCode key = (KeyCode) System.Enum.Parse(typeof(KeyCode), letter.ToUpper());
        return key;
    }
}
