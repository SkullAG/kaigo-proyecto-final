using UnityEngine;
using Core.Actions;
using Core.Characters;

public class CommandItem : Command
{

    public int itemIndex;

    private Inventory _inventory;

    public CommandItem(int id, int itemIndex) : base(id) {

        this.itemIndex = itemIndex;

        _inventory = PartyInventory.current.inventory;

    }

    public override void Execute() {     

        _inventory.Use(itemIndex);    

    }
    
}
