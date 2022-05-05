using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //public Objects scriptableObject;
    public int valueObject;

    public CommandList _commandList;

    [SerializeField]
    public List<Objects> ShopObjects = new List<Objects>();

    private Inventory _inventory;

    private void OnEnable()
    {
        CreateCommands();
    }
    private void Start()
    {
        _inventory = PartyInventory.current.inventory;
    }

    public void GiveObject(int index)
    {
        if (_inventory.coins >= valueObject)
        {
            _inventory.Add(ShopObjects[index]);

            _inventory.coins = _inventory.coins  - valueObject;
        }
    }

    private void CreateCommands()
    {
        CommandShop[] _CShop = new CommandShop[ShopObjects.Count];

        for (int i = 0; i < ShopObjects.Count; i++)
        {
            _CShop[i] = new CommandShop(i) {
                displayName = ShopObjects[i].displayName,
                displayDescription = ShopObjects[i].description,
                shop = this,
                index = i
            };
        }

        _commandList.SetCommands(_CShop);
    }
}
