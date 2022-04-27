using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    public class ActionQueue : MonoBehaviour
    {
        
        [SerializeField] private bool _updating = true;
        [SerializeField] private bool _busy = false;
        [SerializeField] private float _actionTimer = 0;
        [SerializeField] private float _actionSpeed = 1;

        [SerializeField] private GameAction _queuedAction;

        [Space(15)]

        [SerializeField, ReadOnly, Expandable] private GameAction _currentAction;

        private void FixedUpdate() {

            if(_updating) {

                // Update current action processing
                _currentAction?.Update();

                if( _actionTimer >= 1.0f ) {

                    if(!_busy && _queuedAction != null) {

                        _currentAction = _queuedAction;
                        _queuedAction = null;
                        _currentAction.Execute();

                        _currentAction.onActionEnd += OnActionEnd;

                        _busy = true;

                    }

                } else {

                    _actionTimer += Time.deltaTime * _actionSpeed;

                }

            } else {

                _actionTimer = 0;

            }

        }

        private void OnActionEnd() {
            
            _actionTimer = 0;

            _currentAction.onActionEnd -= OnActionEnd; // Stop listening action end event

            _busy = false;

        }

        public void RequestExecution(GameAction action) {

            _queuedAction = action;

        }

        // Overloaded for ease of access :3
        public void RequestExecution(GameAction action, Character actor, Character target) {

            _queuedAction = action;
            _queuedAction.actor = actor;
            _queuedAction.target = target;

        }

    }
    
}

