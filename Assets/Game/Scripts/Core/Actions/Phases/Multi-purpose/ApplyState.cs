using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;
using Core.States;

public class ApplyState : ActionPhase
{
    
    private State _state;
    private string _deathStateName = "Death";

    private bool _healthDepleted = false;

    public ApplyState(State state) {

        this._state = state;

    }

    public override void Start(Character actor, Character target) {

        base.Start(actor, target);

    }
    
    // Update phase's processing
    public override void Update() {

        if(started) {

            if(target != null) {

                target.states.AddState(_state);

                if(target.stats.healthPoints.depleted) {

                    // Add Death state to character
                    target.states.AddState(_deathStateName);

                }

                End();

            }

        }

    }

}
