using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    //public Transform _capsule;
    public Inventory _inventory;    
    private void OnTriggerEnter(Collider other)
    {
        PickableObject _inter = other.GetComponent<PickableObject>();
        if (_inter)
        { 
            //_inventory.huecos.Add(_casilla);
            other.gameObject.SetActive(false);
            _inventory.Add(_inter._obj);
        }      
        //items.Remove("potion");
    }
}
