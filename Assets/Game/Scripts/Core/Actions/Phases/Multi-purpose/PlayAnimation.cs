using UnityEngine;
using Core.Actions;
using Core.Characters;

public class PlayAnimation : ActionPhase
{

    private const string DAMAGE_EVENT_NAME = "ApplyDamage";
    private const string ACTION_ID_PARAM_NAME = "ActionID";
    private const string INSTANT_CAST_PARAM_NAME = "InstantCast";

    private int _actionID;
    private int _animationLayer;
    private bool _instantCast;

    private Animator _animator;
    private AnimationEventSender _sender;

    private bool _customEnd = true;

    public PlayAnimation(int actionID, int layer, bool instantCast) {

        this._actionID = actionID;
        this._animationLayer = layer;
        this._instantCast = instantCast;

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

        if(_animator != null) {

            _animator.SetInteger(ACTION_ID_PARAM_NAME, _actionID); // Send action ID to Animator
            _animator.SetInteger(INSTANT_CAST_PARAM_NAME, _instantCast ? 1 : 0); // Tell animator if action is casted instantly (without charging)

        }

    }

    private void OnEventTriggered(string name) {

        if(name == DAMAGE_EVENT_NAME) {
            End();
        }

    }

    public override void End() {

        // Reset animator parameters
        _animator.SetInteger(ACTION_ID_PARAM_NAME, -1);
        _animator.SetInteger(INSTANT_CAST_PARAM_NAME, -1);

        // Stop listening animation event
        _sender.onEventTriggered -= OnEventTriggered;

        // Give end permision to animation
        _animator.SetTrigger("End");

        base.End();

    }

}
