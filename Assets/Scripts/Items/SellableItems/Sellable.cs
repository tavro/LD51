using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Sellable
{
    public Image img;
    public int price;
    public string name;

    public Sellable(Image img, int price, string name)
    {
        this.img = img;
        this.price = price;
        this.name = name;
    }
}
