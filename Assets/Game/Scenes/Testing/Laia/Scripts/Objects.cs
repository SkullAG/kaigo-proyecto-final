using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory Sctiptable/BaseObject")]
public class Objects : ScriptableObject
{
    public string description;
    public int stackMax;
    public string id;
    public GameObject _gameObject;

    public virtual void Use(Character player)
    {

    }

    /*public GameAction action()
    {

    }*/
}
