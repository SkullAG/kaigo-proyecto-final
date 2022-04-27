using UnityEngine;
using Core.Actions;
using Core.Characters;
using UnityEngine.UI;

public class CommandAction : Command
{

    public int actionIndex = 0;
    
    private Character _selectedCharacter;
    private ActionQueue _queue;
    private Targetter _targetter;
    private GameAction _action;

    public CommandAction(int id, int actionIndex) : base(id) {

        this.actionIndex = actionIndex;

    }

    public override void Execute() {     

        // Get current selected character
        PartyManager _manager = PartyManager.current;

        _selectedCharacter = _manager.PartyMembers[_manager.selectedCharacter].GetComponent<Character>();

        if(_selectedCharacter != null) {

            // Get action by index
            _action = _selectedCharacter.actions.GetAction(actionIndex);

            _action.actor = _selectedCharacter;

            if(_action.hasTargetSelection) {

                // Enable target selection
                _targetter.Enable();
                _targetter.onTargetSelect += OnTargetSelected;

            } else {

                // If action has no target selection, target is actor
                _action.target = _selectedCharacter;
                _queue.RequestExecution(_action);
                
            }

        }

    }

    // When target is selected, request action execution
    private void OnTargetSelected(Character selected) {

        _action.target = selected;
        _queue.RequestExecution(_action);

        // When target is selected, unsubscribe from targetter event
        _targetter.onTargetSelect -= OnTargetSelected;

    }
    
}
