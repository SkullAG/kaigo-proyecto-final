using UnityEngine;
using Core.Actions;
using Core.Characters;

public class CommandAction : Command
{
    
    public GameAction action;
    
    private Character _selectedCharacter;
    private ActionQueue _queue;
    private Targetter _targetter;

    private void Awake() {

        // Get current selected character
        var _manager = PartyManager.current;
        
        _selectedCharacter = _manager.PartyMembers[_manager.selectedCharacter].GetComponent<Character>();

    }

    public override void Execute() {     

        if(_selectedCharacter != null) {

            action.actor = _selectedCharacter;

            if(action.hasTargetSelection) {

                // Enable target selection
                _targetter.Enable();
                _targetter.onTargetSelect += OnTargetSelected;

            } else {

                // If action has no target selection, target is actor
                action.target = _selectedCharacter;
                _queue.RequestExecution(action);
                
            }

        }

    }

    // When target is selected, request action execution
    private void OnTargetSelected(Character selected) {

        action.target = selected;
        _queue.RequestExecution(action);

    }
    
}
