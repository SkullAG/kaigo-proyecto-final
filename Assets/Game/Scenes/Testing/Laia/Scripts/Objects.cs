using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory Sctiptable/BaseObject")]
public class Objects : ScriptableObject, IListableElement
{
    public string description;
    public int stackMax;
    public string id;
    public GameObject _gameObject;

    // Añadido: propiedades que utiliza la clase ButtonList
    public string displayName => id;
    public string displayDescription => description;

    public virtual void Use(Character player)
    {

    }

    /*public GameAction action()
    {

    }*/
}
