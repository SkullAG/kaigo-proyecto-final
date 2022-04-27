using System.Collections.Generic;
using UnityEngine;
using Core.Actions;

[RequireComponent(typeof(CommandList))]
public class UIActionList : MonoBehaviour
{

    private CommandList _commandList;
    private PartyManager _partyManager;
    private ActionList _actionList;

    private int _lastSelectedCharacter = -1;

    private void Start() {

        _commandList = GetComponent<CommandList>();
        _partyManager = PartyManager.current;

    }

    private void Update() {

        if(_lastSelectedCharacter != _partyManager.selectedCharacter) {

            _actionList = _partyManager.PartyMembers[_partyManager.selectedCharacter].GetComponent<ActionList>();
        
            CreateCommands();

        }

        _lastSelectedCharacter = _partyManager.selectedCharacter;

    }

    private void CreateCommands() {

        List<CommandAction> _commands = new List<CommandAction>();

        // Create commands for each available action
        for(int i = 0; i < _actionList.actions.Length; i++) {

            CommandAction _c = new CommandAction(0, i) {

                displayName = _actionList.actions[i].displayName,
                displayDescription = _actionList.actions[i].description

            };

            _commands.Add(_c);

        }
        
        // Send commands to be listed
        _commandList.SetCommands(_commands.ToArray());

    }

}
