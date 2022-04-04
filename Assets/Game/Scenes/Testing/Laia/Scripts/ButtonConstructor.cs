using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonConstructor : MonoBehaviour
{
    //public GameObject buttons;
    public GameObject _button;
    public Inventory _inventory;

    public void OnEnable()
    {
        _inventory.SendOnInventoryChange.AddListener(UpdateList);
    }

    [SerializeField]
    public List<GameObject> buttonslist = new List<GameObject>();
    public void UpdateList(Dictionary<string, Inventory.Casilla> huecos)
    {
        GameObject but = Instantiate(_button, transform.position, transform.rotation);

        but.transform.parent = transform;

        buttonslist.Add(but);
    }
}
