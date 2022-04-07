using UnityEngine;
using Core.Actions;
using Core.Characters;

public class PlayAnimation : ActionPhase
{

    private string _animationStateName;
    private float _endTime;

    private Animator _animator;

    public PlayAnimation(string stateName, float _endTime = -1) {

        this._animationStateName = stateName;

        if(_endTime > 0) {

            this._endTime = _endTime;

        }

    }

    public override void Update(Character actor, Character[] targets)
    {

        if(_animator == null) {

            _animator = actor.gameObject.GetComponentInChildren<Animator>();
            _animator.Play(_animationStateName);

        }

        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_animationStateName)) {

            var _info = _animator.GetCurrentAnimatorStateInfo(0);
            float _time = _info.normalizedTime;
            float _length = _info.length;

            if( _time >= _endTime / _length ) {

                End();

            }

        }

    }

}
