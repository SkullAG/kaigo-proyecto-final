using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory Sctiptable/LifePotion")]
public class Potion : Objects
{
    public int life = 5;
    public override void Use()
    {
        Debug.Log("Se te ha sumado " + life );
    }
}
