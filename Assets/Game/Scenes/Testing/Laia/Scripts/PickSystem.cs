using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSystem : MonoBehaviour
{
    //public Inventory _inventory;
    public bool PickObj(Objects obj)
    {
        return PartyInventory.current.inventory.Add(obj);
    }
}
