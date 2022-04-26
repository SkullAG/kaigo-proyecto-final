using UnityEngine;
using Core.Characters;
using Core.Actions;

[RequireComponent(typeof(ButtonList))]
public class UIItemList : MonoBehaviour
{

    private Inventory _inventory;
    private ButtonList _selectableList;

    private int _lastItemCount;

    private void Start() {

        _selectableList = GetComponent<ButtonList>();
        _inventory = PartyInventory.current.inventory;

    }

    private void Update() {

        if(_inventory.items.Length != _lastItemCount) {

            SetElements();

        }

        _lastItemCount = _inventory.items.Length;

    }

    private void SetElements() {

        // Set elements in UI list
        var items = _inventory.items;
        _selectableList.SetElements(items);

    }

}
