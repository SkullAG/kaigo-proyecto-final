using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSystem : MonoBehaviour
{
    public Inventory _inventory;
    public bool PickObj(Objects obj)
    {
        return _inventory.Add(obj);
    }
}
