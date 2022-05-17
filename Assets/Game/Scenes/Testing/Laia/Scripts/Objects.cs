using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Game/Inventory/Consumable")]
public class Objects : ScriptableObject
{   
    public string displayName;
    public string id;
    public int value;

    [TextArea]
    public string description;

    public int stackMax;

    public ActionReference actionReference;

    private Character _selectedCharacter;

    public void Use(Character target)
    {
        //Debug.Log("a");
        _selectedCharacter = PartyManager.current.GetSelectedCharacter();
        

        // Get selected character action queue and request execution of
        // the action assigned to this item.
        var _queue = _selectedCharacter.GetComponent<ActionQueue>();

        _queue.RequestExecution(actionReference, _selectedCharacter, target, true);

        Debug.Log("Using item " + displayName + " on " + target.gameObject.name);
    }

}
