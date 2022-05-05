using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandShop : Command
{
    public int index;
    public Shop shop;

    public CommandShop(int id) : base(id)
    {
        
    }

    public override void Execute()
    {
        shop.GiveObject(index);
    }
}
