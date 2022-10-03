using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Letter Data", menuName = "Letter Data", order = 1)]
public class LetterData : ScriptableObject
{
	public int day;
    public string subject;
    [TextArea] public string body;
    [TextArea] public string author;
}

[System.Serializable]
public class LetterDataTwo
{
	public int day;
    public string subject;
    [TextArea] public string body;
    [TextArea] public string author;
}
