using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory Sctiptable/BaseObject")]
public class Objects : ScriptableObject
{
    public string description;
    public int stackMax;
    public string names;
    public string id;
    public GameObject _gameObject;

    public virtual void Use()
    {

    }

    /*public GameAction action()
    {

    }*/
}
