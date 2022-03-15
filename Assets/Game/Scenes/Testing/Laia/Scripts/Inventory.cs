using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{    
    public Transform _capsule;
    public Button _button;
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

    public void Use(int value)
    {
        Debug.Log("Usando " + huecos[value].objeto.names);
        huecos[value].objeto.Use();
        huecos.RemoveAt(value);
    }

    public void Quit(int value) 
    {
        Debug.Log("Quitando " + huecos[value].objeto.names);
        huecos.RemoveAt(value);
    }

}
