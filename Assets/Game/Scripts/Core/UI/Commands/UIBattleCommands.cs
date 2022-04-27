using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;

public class UIBattleCommands : MonoBehaviour
{

    [SerializeField] private CommandList _actionCommands;
    [SerializeField] private CommandList _itemCommands;

    [SerializeField] private int _attackActionIndex = 0;
    
    private void Start() {

        CreateCommands();

    }

    private void CreateCommands() {

        PartyManager _partyManager = PartyManager.current;
        ActionList _actionList = _partyManager.PartyMembers[_partyManager.selectedCharacter].GetComponent<ActionList>();

        Command[] _commands = {

            // Attack command
            new CommandAction(0, _attackActionIndex) { 
                
                displayName = "Attack",
                displayDescription = _actionList.actions[_attackActionIndex].description, // Use description of action
                
            },

            // Ability list command
            new CommandFoldout(1, _actionCommands) {

                displayName = "Abilities",
                displayDescription = "List of available abilities for the selected character.",

            },

            // Item list command
            new CommandFoldout(2, _itemCommands) {

                displayName = "Items",
                displayDescription = "List of all items in party's inventory.",

            },

        };

        GetComponent<CommandList>().SetCommands(_commands);

    }

}
