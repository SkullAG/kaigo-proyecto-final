using UnityEngine;
using Core.Actions;
using Core.Characters;

public class CommandItem : Command
{

    [SerializeField]
    private int _itemIndex;

    public CommandItem(int id, int itemIndex) : base(id) {

        _itemIndex = itemIndex;

    }

    public override void Execute() {     

        // Code responsible of using the selected item    

    }
    
}
