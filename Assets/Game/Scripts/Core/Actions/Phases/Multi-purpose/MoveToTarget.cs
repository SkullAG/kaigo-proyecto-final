using Core.Actions;
using Core.Characters;
using UnityEngine;
public class MoveToTarget : ActionPhase
{

    private float _distanceToStop;
    private float _angleToStop = 2;

    public NavBodySistem _navigator;

    private bool _stopped;

    public MoveToTarget(float distanceToStop) {

        this._distanceToStop = distanceToStop;

    }

    public override void Update()
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

                    //Debug.Log("Moving");

                    // Move
                    _navigator.ObjectivePoint = target.transform.position;

                    _stopped = false;

                } else {

                    //Debug.Log("Finished moving?");
                    
                    if(!_stopped) Stop();
                    
                    LookAtTarget();

                }

            }

        }

    }

    private void LookAtTarget() {

        //Debug.Log("Looking at target");

        Vector3 _dir = (target.transform.position - actor.transform.position).normalized;

        float _angle = _navigator.RotateTowards(_dir);

        //Debug.Log("Angle: " + _angle);

        if( CustomMath.Aproximately(_angle, 0, _angleToStop) ) {
        
            End();

        } 

    } 

    private void Stop() {

        //Debug.Log("Stopping");

        // Stop
        _navigator.Objective = null;
        _navigator.ObjectivePoint = _navigator.transform.position;

        _stopped = true;

    }

}
