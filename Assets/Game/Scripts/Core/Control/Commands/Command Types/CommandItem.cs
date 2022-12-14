using System.Linq;
using UnityEngine;
using Core.Actions;

[System.Serializable]
public class CommandItem : Command
{

    public int itemIndex;

    private Inventory _inventory;
    private Inventory.Casilla _slot;
    private CommandList _list;

    private GameAction _action;

    private bool _alreadyExecuting;

    public CommandItem(int id, int itemIndex) : base(id) {

        this.itemIndex = itemIndex;

        _inventory = PartyInventory.current.inventory;
        _slot = _inventory.huecos.Values.ToArray()[itemIndex];

        _list = CommandController.current.GetSubcommandList(CommandController.SubcommandType.items);

        _inventory.effectApplied += UpdateName;
        _inventory.itemAdded += UpdateName;
        _inventory.itemRemoved += UpdateName;

    }

    ~CommandItem() {

        _inventory.effectApplied -= UpdateName;
        _inventory.itemAdded -= UpdateName;
        _inventory.itemRemoved -= UpdateName;

    }

    public override void Execute() {     

        _inventory.Use(itemIndex);    

        UpdateName();

    }

    private void UpdateName() {

        Debug.Log("Actualizando nombre de objeto");

        // Add x for stacks over 1 
        if(_slot.stack > 1) {

            displayName = _slot.objeto.displayName + " x" + _slot.stack;

        } else {

            displayName = _slot.objeto.displayName;

        }

        // Update command
        _list.ReplaceCommand(id, this);

    }
    
}
