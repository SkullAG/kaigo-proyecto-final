using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    //public Objects scriptableObject;
    //public int valueObject;

    public CommandList _commandList;

    [SerializeField]
    public List<Objects> ShopObjects = new List<Objects>();

    private Inventory _inventory;

    public TextMeshProUGUI _textMesh;

    public string MoneyIconId;

    public void OnEnable()
    {
        Debug.Log("Created singleton shop");

        CreateCommands();
        _textMesh.text = _inventory.coins.ToString();
    }
    
    private void Start()
    {
        _inventory = PartyInventory.current.inventory;

        _textMesh.text = _inventory.coins.ToString();
    }

    public void GiveObject(int index)
    {
        if (_inventory.coins >= ShopObjects[index].value)
        {
            _inventory.Add(ShopObjects[index]);

            _inventory.coins = _inventory.coins  - ShopObjects[index].value;

            _textMesh.text = _inventory.coins.ToString();
        }
    }

    private void CreateCommands()
    {
        CommandShop[] _CShop = new CommandShop[ShopObjects.Count];

        for (int i = 0; i < ShopObjects.Count; i++)
        {
            _CShop[i] = new CommandShop(i) {
                displayName = ShopObjects[i].displayName + " <sprite name=" + MoneyIconId + ">" + ShopObjects[i].value,
                displayDescription = ShopObjects[i].description,
                shop = this,
                index = i
            };
        }

        _commandList.SetCommands(_CShop);
    }
}
