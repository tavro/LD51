using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buyable
{
    public Image img;
    public int price;
    public string name;

    public Buyable(Image img, int price, string name)
    {
        this.img = img;
        this.price = price;
        this.name = name;
    }
}
