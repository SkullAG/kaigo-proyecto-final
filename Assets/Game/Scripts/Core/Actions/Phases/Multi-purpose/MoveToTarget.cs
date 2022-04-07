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

    public override void Update(Character actor, Character[] targets)
    {
        
        if(_navigator == null) {

            _navigator = actor.GetComponent<NavBodySistem>();

        } else {

            float _dist = Vector3.Distance(actor.transform.position, targets[0].transform.position);

            if(_dist > _distanceToStop) {

                _navigator.ObjectivePoint = targets[0].transform.position;

            } else {

                _navigator.Objective = null;
                _navigator.ObjectivePoint = _navigator.transform.position;

                End();

            }

        }


    }

}
