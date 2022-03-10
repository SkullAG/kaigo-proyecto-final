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
            Inventory.Casilla _casilla =  new Inventory.Casilla();
            _casilla.objeto = _inter._obj;
            _casilla.stack = 1;
            _inventory.huecos.Add(_casilla);
            other.gameObject.SetActive(false);
        }      
        //items.Remove("potion");
    }
}
