using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "Game/Inventory/Consumable")]
public class Objects : ScriptableObject
{   
    public string id;
    public string displayName;
    public string description;

    public int stackMax;

    public GameAction itemAction;

    private Character _selectedCharacter;

    public void Use(Character target)
    {

        _selectedCharacter = PartyManager.current.GetSelectedCharacter();

        // Get selected character action queue and request execution of
        // the action assigned to this item.
        var _queue = _selectedCharacter.GetComponent<ActionQueue>();
        _queue.RequestExecution(itemAction, _selectedCharacter, target);

        Debug.Log("Using item " + displayName + " on " + target.gameObject.name);

    }

}
