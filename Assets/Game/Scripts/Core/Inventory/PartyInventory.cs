using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInventory : Singleton<PartyInventory>
{
    
    [HideInInspector]
    public Inventory inventory;

    public void Awake() {

        inventory = GetComponent<Inventory>();

    }

}
