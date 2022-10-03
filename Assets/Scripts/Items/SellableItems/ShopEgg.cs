using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class ShopEgg : Sellable
{
    public ShopEgg(Image img, int price, string name) : base(img, price, name)
    {
    }
}
