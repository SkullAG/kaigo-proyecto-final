using UnityEngine;
using Core.Actions;
using Core.Characters;

public class PlayAnimation : ActionPhase
{

    private string _animationStateName;
    //private float _endTime;

    private Animator _animator;
    private AnimationEventString _eventComm;

    private bool _customEnd = true;

    public PlayAnimation(string stateName, float _endTime = -1) {

        this._animationStateName = stateName;

    }

    public override void Update(Character actor, Character target)
    {

        if(_animator == null) {

            _animator = actor.gameObject.GetComponentInChildren<Animator>();

            Debug.Log("Playing animation: " + _animationStateName);
            _animator.Play(_animationStateName);

        }

        if(_eventComm == null) {

            _eventComm = actor.transform.GetChild(0).GetComponent<AnimationEventString>();

            if(_eventComm != null) {

                // Subscribe to animation event handler

                _eventComm.onEventTriggered += OnAnimationEvent;

            } else {

                _customEnd = false;

            }

        }

        if(!_customEnd) {

            // Trigger phase end when animation ends

            if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_animationStateName)) {

                var _info = _animator.GetCurrentAnimatorStateInfo(0);

                if( _info.normalizedTime >= 1 ) {

                    End();

                }

            }

        }

    }

    public override void End() {

        base.End();

        _eventComm.onEventTriggered -= OnAnimationEvent;

    }

    private void OnAnimationEvent(string name) {

        if(name == "End") {

            End();

        }

    }

}
