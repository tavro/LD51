using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryProcessor
{
    public static string PATH = Application.dataPath + "/Text/dictionary.txt";

    public static string GetWordOfLength(int length, KeyCode code) {
        string[] lines = System.IO.File.ReadAllLines(PATH);
        List<string> result = new List<string>();
        foreach(string line in lines) {
            if(line.Length == length) {
                if(line[0] == code.ToString().ToLower()[0]) { //First letter is same
                    result.Add(line);
                }    
            }
        }
        return result[Random.Range(0, result.Count)];
    }
}
