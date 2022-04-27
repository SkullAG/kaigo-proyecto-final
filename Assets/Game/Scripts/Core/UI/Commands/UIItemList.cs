using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;
using System.Linq;

[RequireComponent(typeof(CommandList))]
public class UIItemList : MonoBehaviour
{

    [SerializeField] private Objects _testItem;

    private Inventory _inventory;
    private CommandList _commandList;

    private int _lastItemCount;

    private void Start() {

        _commandList = GetComponent<CommandList>();
        _inventory = PartyInventory.current.inventory;

        _inventory.Add(_testItem);
        _inventory.Add(_testItem);
        _inventory.Add(_testItem);

    }

    private void Update() {

        if(_inventory.items.Length != _lastItemCount) {

            CreateCommands();

        }

        _lastItemCount = _inventory.items.Length;

    }

    private void CreateCommands() {

        // Get slots that aren't empty
        Inventory.Casilla[] _slots = _inventory.huecos.Values.Where(x => x.stack != 0).ToArray();
        
        List<CommandItem> _commands = new List<CommandItem>();

        // Create commands for each available used slot in inventory
        for(int i = 0; i < _slots.Length; i++) {

            string _initialName =  _slots[i].objeto.displayName;
            
            // Set name to include stack size initially
            if(_slots[i].stack > 0) {
                _initialName += " x" + _slots[i].stack;
            }

            CommandItem _c = new CommandItem(i, i) {

                displayName = _initialName,
                displayDescription = _slots[i].objeto.description

            };

            _commands.Add(_c);

        }
        
        // Send commands to be listed
        _commandList.SetCommands(_commands.ToArray());

    }

}
