using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    public Transform _capsule;
    public void boton()    
    {        
        _capsule.gameObject.SetActive(true);
    }

    [Serializable]
    public struct Casilla
    {
        public Objects objeto;
        public int stack;
    };

    [SerializeField]
    public List<Casilla> huecos = new List<Casilla>();
    //public List<string> items = new List<string>();

    public void Use()
    {
        Debug.Log("Usando " + huecos[0].objeto.names);
        huecos[0].objeto.Use();
    }
}
