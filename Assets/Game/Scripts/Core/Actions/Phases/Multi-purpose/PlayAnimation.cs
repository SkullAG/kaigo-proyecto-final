using UnityEngine;
using Core.Actions;
using Core.Characters;

public class PlayAnimation : ActionPhase
{

    private const string END_EVENT_NAME = "End";
    private const string END_STATE_NAME = "End";
    private const string ACTION_ID_PARAM_NAME = "ActionID";
    private const string INSTANT_CAST_PARAM_NAME = "InstantCast";

    private string _stateName;
    private bool _blockMovement;

    private Animator _animator;
    private AnimationEventSender _sender;

    private bool _started = false;

    public PlayAnimation(string stateName, bool blockMovement) {

        _stateName = stateName;
        _blockMovement = blockMovement;

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

            // Attempt to stop movement
            if(_blockMovement) actor.GetComponent<NavBodySistem>().isParalized = true;

            _started = true;

        }

        // End phase if animator reached End state
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(END_STATE_NAME)) {

            End();

        }

    }

    // Trigger end of phase by animation event
    private void OnEventTriggered(string name) {

        if(name == END_EVENT_NAME) {
            End();
        }

    }

    public override void End() {

        // Stop listening animation event
        _sender.onEventTriggered -= OnEventTriggered;

        // Attempt to resume movement
        if(_blockMovement) actor.GetComponent<NavBodySistem>().isParalized = false;

        base.End();

    }

}
