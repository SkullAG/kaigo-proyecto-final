using UnityEngine;
using Core.Actions;
using Core.Characters;
using UnityEngine.UI;

[System.Serializable]
public class CommandAction : Command
{

    public int actionIndex = 0;
    
    private Character _selectedCharacter;
    private GameAction _action;

    private ActionQueue _queue;

    public CommandAction(int id, int actionIndex) : base(id) {

        this.actionIndex = actionIndex;

    }

    public override void Execute() {     

        // Get current selected character
        PartyManager _manager = PartyManager.current;

        _selectedCharacter = _manager.GetSelectedCharacter();
        _queue = _selectedCharacter.GetComponent<ActionQueue>();

        if(_selectedCharacter != null) {

            // Get action by index
            _action = _selectedCharacter.actions.GetAction(actionIndex);

            _action.actor = _selectedCharacter;

            if(_action.hasTargetSelection) {

                // Enable target selection
                Targetter.current.Enable();
                Targetter.current.onTargetSelect += OnTargetSelected;

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
        Targetter.current.onTargetSelect -= OnTargetSelected;

    }
    
}
