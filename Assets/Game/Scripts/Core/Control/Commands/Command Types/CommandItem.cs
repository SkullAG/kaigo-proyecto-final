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

    }

    ~CommandItem() {

        _inventory.effectApplied -= UpdateName;

    }

    public override void Execute() {     

        if(!_alreadyExecuting) { // Prevent double execution

            _action = _inventory.items[itemIndex].itemAction;
            _action.onActionEnd += OnActionEnd;

            _inventory.Use(itemIndex);    

            UpdateName();

            _alreadyExecuting = true;

        }


    }

    private void OnActionEnd() {

        _alreadyExecuting = false;

        _action.onActionEnd -= OnActionEnd;

    }

    private void UpdateName() {

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
