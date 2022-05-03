using UnityEngine;
using Core.Actions;
using Core.Characters;

public class PlayAnimation : ActionPhase
{

    private const string DAMAGE_EVENT_NAME = "ApplyDamage";
    private const string ACTION_ID_PARAM_NAME = "ActionID";
    private const string INSTANT_CAST_PARAM_NAME = "InstantCast";

    private string _stateName;

    private Animator _animator;
    private AnimationEventSender _sender;

    private bool _customEnd = true;
    private bool _started = false;

    public PlayAnimation(string stateName) {

        _stateName = stateName;

    }

    public override void Start(Character actor, Character target) {

        base.Start(actor, target);

        _animator = actor.gameObject.GetComponentInChildren<Animator>();
        _sender = _animator.GetComponent<AnimationEventSender>();

        if(_sender != null) {

            // Listen to the animation event
            _sender.onEventTriggered += OnEventTriggered;

        }

    }

    public override void Update() {

        if(_animator != null && !_started) {

            // Play state with action name
            _animator.Play(_stateName, 0);

            _started = true;

        }

    }

    private void OnEventTriggered(string name) {

        if(name == DAMAGE_EVENT_NAME) {
            End();
        }

    }

    public override void End() {

        // Stop listening animation event
        _sender.onEventTriggered -= OnEventTriggered;

        // Give end permision to animation
        _animator.SetTrigger("End");

        base.End();

    }

}
