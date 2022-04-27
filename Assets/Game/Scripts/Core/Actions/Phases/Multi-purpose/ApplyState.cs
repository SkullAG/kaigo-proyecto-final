using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;
using Core.States;

public class ApplyState : ActionPhase
{
    
    private State[] _states;
    private string _deathStateName = "Death";

    private bool _healthDepleted = false;

    public ApplyState(State[] states) {

        this._states = states;

    }
    
    // Update phase's processing
    public override void Update() {

        if(started) {

            // End action if there are no states to apply
            if(_states == null || _states.Length == 0) {

                End();
                return;

            }

            if(target != null) {

                // Applies each state
                foreach (var state in _states) {

                    target.states.AddState(state);

                }

                // Maybe this shouldn't be here :S
                if(target.stats.healthPoints.depleted) {

                    // Add Death state to character
                    target.states.AddState(_deathStateName);

                }

                End();

            }

        }

    }

}
