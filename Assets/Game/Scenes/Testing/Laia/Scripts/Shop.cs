using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Objects scriptableObject;

    [SerializeField]
    public List<Objects> ShopObjects = new List<Objects>();

    public Inventory _inventory;
    public void GiveObject(int index)
    {
        _inventory.Add(ShopObjects[index]);
    }
}
