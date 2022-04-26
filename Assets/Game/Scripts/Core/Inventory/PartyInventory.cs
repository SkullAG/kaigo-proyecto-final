using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInventory : Singleton<PartyInventory>
{
    
    [HideInInspector]
    public Inventory inventory;

    public override void Awake() {

        base.Awake();

        inventory = GetComponent<Inventory>();

    }

}
