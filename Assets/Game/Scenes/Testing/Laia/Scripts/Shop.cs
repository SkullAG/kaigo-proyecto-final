using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Objects scriptableObject;
    public int valueObject;

    [SerializeField]
    public List<Objects> ShopObjects = new List<Objects>();

    public Inventory _inventory;
    public void GiveObject(int index)
    {
        if (_inventory.coins >= valueObject)
        {
            _inventory.Add(ShopObjects[index]);

            _inventory.coins = _inventory.coins  - valueObject;
        }
            
               
    }
}
