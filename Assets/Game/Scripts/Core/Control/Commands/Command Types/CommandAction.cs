using UnityEngine;
using Core.Actions;
using Core.Characters;
using UnityEngine.UI;
using System;

[System.Serializable]
public class CommandAction : Command
{

    public int actionIndex = 0;
    
    private Character _selectedCharacter;
    private GameAction _action;

    private ActionQueue _queue;

    private bool _alreadyExecuted;

    public CommandAction(int id, int actionIndex) : base(id) {

        this.actionIndex = actionIndex;

    }

    public override void Execute() {     

        if(!_alreadyExecuted) { // Prevent accidental execution

            _alreadyExecuted = true;

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

                    // Listen to targetter events
                    Targetter.current.targetConfirmed += OnTargetConfirmed;
                    Targetter.current.targetCancelled += OnTargetCancelled;

                } else {

                    // If action has no target selection, target is actor
                    _queue.RequestExecution(_action.displayName, _selectedCharacter, _selectedCharacter);
                    
                }

            }


        }


    }

    // When target is selected, request action execution
    private void OnTargetConfirmed(Character selected) {

        _queue.RequestExecution(_action.displayName, _selectedCharacter, selected);

        // When target is selected, unsubscribe from targetter event
        Targetter.current.targetConfirmed -= OnTargetConfirmed;
        Targetter.current.targetCancelled -= OnTargetCancelled;

        _alreadyExecuted = false;

    }

    private void OnTargetCancelled(Character lastTarget) {

        Debug.Log("Targetting is cancelled!");

        Targetter.current.targetConfirmed -= OnTargetConfirmed;
        Targetter.current.targetCancelled -= OnTargetCancelled;

        _alreadyExecuted = false;

    }
    
}
