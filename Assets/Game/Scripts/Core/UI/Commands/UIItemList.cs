using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;

[RequireComponent(typeof(CommandList))]
public class UIItemList : MonoBehaviour
{

    private Inventory _inventory;
    private CommandList _commandList;

    private int _lastItemCount;

    private void Start() {

        _commandList = GetComponent<CommandList>();
        _inventory = PartyInventory.current.inventory;

    }

    private void Update() {

        if(_inventory.items.Length != _lastItemCount) {

            CreateCommands();

        }

        _lastItemCount = _inventory.items.Length;

    }

    private void CreateCommands() {

        // Get inventory items
        var _items = _inventory.items;
        
        List<CommandItem> _commands = new List<CommandItem>();

        // Create commands for each available item in inventory
        for(int i = 0; i < _items.Length; i++) {

            CommandItem _c = new CommandItem(0, i) {

                displayName = _items[i].id,
                displayDescription = _items[i].description
                
            };

            _commands.Add(_c);

        }
        
        // Send commands to be listed
        _commandList.SetCommands(_commands.ToArray());

    }

}
