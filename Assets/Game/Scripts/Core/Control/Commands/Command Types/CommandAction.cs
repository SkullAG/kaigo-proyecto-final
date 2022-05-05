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
    private ActionQueue _queue;

    private ActionReference _reference;

    public CommandAction(int id, int actionIndex) : base(id) {

        this.actionIndex = actionIndex;

        // Get current selected character
        PartyManager _manager = PartyManager.current;

        _selectedCharacter = _manager.GetSelectedCharacter();
        _queue = _selectedCharacter.GetComponent<ActionQueue>();

        // Get action reference by index
        _reference = _selectedCharacter.actions.GetReference(actionIndex);

    }

    public override void Execute() {     

        if(_selectedCharacter != null) {
            
            // Check if action can actually be used by selected character
            if(_reference.sharedAction.IsUsableBy(_selectedCharacter)) {

                // Don't instance an action if it's being passed to the queue!

                if(_reference.sharedAction.hasTargetSelection) {

                    // Enable target selection
                    Targetter.current.Enable();

                    // Listen to targetter events
                    Targetter.current.targetConfirmed += OnTargetConfirmed;
                    Targetter.current.targetCancelled += OnTargetCancelled;

                } else {

                    // If action has no target selection, target is actor
                    _queue.RequestExecution(_reference, _selectedCharacter, _selectedCharacter);
                    
                }

            }


        }


    }

    // When target is selected, request action execution
    private void OnTargetConfirmed(Character selected) {

        _queue.RequestExecution(_reference, _selectedCharacter, selected);

        // When target is selected, unsubscribe from targetter event
        Targetter.current.targetConfirmed -= OnTargetConfirmed;
        Targetter.current.targetCancelled -= OnTargetCancelled;

    }

    private void OnTargetCancelled(Character lastTarget) {

        Debug.Log("Targetting is cancelled!");

        Targetter.current.targetConfirmed -= OnTargetConfirmed;
        Targetter.current.targetCancelled -= OnTargetCancelled;

    }

    public override bool IsExecutable() {

        if(_reference != null) {

            var _action = (BattleAction)_reference.sharedAction;

            int _hp = _selectedCharacter.stats.healthPoints.value - _action.costOverHP;
            int _ap = _selectedCharacter.stats.actionPoints.value - _action.costOverAP;

            return _hp >= 0 && _ap >= 0;

        } else {

            return false;

        }

    }
    
}
