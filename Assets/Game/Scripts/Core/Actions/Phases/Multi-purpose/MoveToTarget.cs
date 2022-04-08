using Core.Actions;
using Core.Characters;
using UnityEngine;
public class MoveToTarget : ActionPhase
{

    private float _distanceToStop;

    public NavBodySistem _navigator;

    public MoveToTarget(float distanceToStop) {

        this._distanceToStop = distanceToStop;

    }

    public override void Update(Character actor, Character target)
    {

        if(_navigator == null) {

            _navigator = actor.GetComponent<NavBodySistem>();

        } else {

            if(target == null) { 

                Stop();
                return;

            }

            if(target != null) {

                float _dist = Vector3.Distance(actor.transform.position, target.transform.position);

                if( _dist > _distanceToStop && _distanceToStop > 0 ) {

                    // Move
                    _navigator.ObjectivePoint = target.transform.position;

                } else {
                    
                    Stop();
                    End();

                }

            }

        }

    }

    private void Stop() {

        // Stop
        _navigator.Objective = null;
        _navigator.ObjectivePoint = _navigator.transform.position;

    }

}
