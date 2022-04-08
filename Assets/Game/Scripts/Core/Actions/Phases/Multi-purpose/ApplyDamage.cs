using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;
using Core.States;

public class ApplyDamage : ActionPhase
{
    
    private int _damage;
    private string _deathStateName = "Death";

    private bool _healthDepleted = false;

    public ApplyDamage(int damage) {

        this._damage = damage;

    }

    public override void Start(Character actor, Character target) {

        base.Start(actor, target);

    }
    
    // Update phase's processing
    public override void Update() {

        if(started) {

            if(target != null) {

                target.stats.healthPoints.value -= _damage;

                if(target.stats.healthPoints.depleted) {

                    // Add Death state to character
                    target.states.AddState(_deathStateName);

                }

                End();

            }

        }

    }

}
