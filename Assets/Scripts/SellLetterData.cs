using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sell Letter Data", menuName = "Sell Letter Data", order = 2)]
public class SellLetterData : ScriptableObject
{
	public KeyCode sellButton;
    public int amount;
    public string subject;
    [TextArea] public string body;
    [TextArea] public string author;
}
