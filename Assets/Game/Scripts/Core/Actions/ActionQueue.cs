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

            // When action ends, timer resets
            _actionTimer = 1;

            _busy = false;

        }

        // Returns boolean indicating execution success
        public bool RequestExecution(string actionName, Character actor, Character target) {
            
            ActionReference _ref = _actionList.GetReference(actionName);

            if(_ref != null) {

                // Instantiate action from reference and set as queued
                _queuedAction = _ref.Instantiate(actor, target); 
                
                return true;

            } else {

                Debug.LogError("Action requested is not in the character's action list or it's invalid.", this);

                return false;

            }

        }

    }
    
}

