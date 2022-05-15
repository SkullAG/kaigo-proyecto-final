using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    public class ActionQueue : MonoBehaviour
    {
        
        [SerializeField] private bool _updating = true;
        [SerializeField] private bool _busy = false;
        [SerializeField] private bool _ready = true;

        [SerializeField, Range(0, 1)] private float _actionSpeed = 1; // 1 means no action wait
        [SerializeField, ReadOnly] private float _actionTimer = 0;

        [SerializeField] private GameAction _queuedAction;

        [Space(15)]

        [SerializeField, ReadOnly, Expandable] private GameAction _currentAction;
        
        private ActionList _actionList;
        
        public Character currentTarget;
        public bool isPerformingAction => _busy;
        public bool isReady => _ready;

        public float actionSpeed { set => _actionSpeed = Mathf.Clamp01(value); }

        private void Awake() {

            _actionList = GetComponent<ActionList>();

        }

        private void FixedUpdate() {

            if(_updating) {

                if(!_busy) {

                    _actionTimer -= Time.deltaTime * _actionSpeed;

                    // If the action timer is 0, character can execute actions
                    if(_actionTimer <= 0) {

                        _actionTimer = 0;
                        _ready = true;

                        // If there's a queued action, execute it!
                        if(_queuedAction != null) {

                            _currentAction = _queuedAction;
                            _queuedAction = null;
                            _currentAction.Execute();

                            _currentAction.onActionEnd += OnActionEnd;

                            _busy = true;
                            _ready = false;

                        }

                    }

                } else {

                    // Update current action processing if busy
                    _currentAction?.Update();

                }       

            } else {

                _actionTimer = 0;

            }

        }

        private void OnActionEnd() {

            _currentAction.onActionEnd -= OnActionEnd; // Stop listening action end event

            currentTarget.isBeingTargetted = false;
            currentTarget.targettedBy = null;
            currentTarget = null; 

            // When action ends, timer resets
            _actionTimer = 1;

            _busy = false;

        }

        // Returns boolean indicating execution success
        public bool RequestExecution(ActionReference reference, Character actor, Character target, bool freeUse = false, bool throwError = true) {
            
            // Free use allows to pass action without checking action list first
            if(!freeUse) {
                
                // If action list has the reference (what is: character can perform action)
                if(_actionList.Contains(reference)) {

                    currentTarget = target;
                    currentTarget.isBeingTargetted = true;
                    currentTarget.targettedBy = actor;

                    _queuedAction = reference.Instantiate(actor, target);
                    
                    return true;

                } else {

                    if(throwError) {

                        Debug.LogError("Action requested is not in the character's action list or it's null.", this);

                    }

                    return false;

                }

            } else {

                // Instantiate without checking
                currentTarget = target;
                currentTarget.isBeingTargetted = true;
                currentTarget.targettedBy = actor;

                _queuedAction = reference.Instantiate(actor, target);

                return true;

            }

        }

    }
    
}

