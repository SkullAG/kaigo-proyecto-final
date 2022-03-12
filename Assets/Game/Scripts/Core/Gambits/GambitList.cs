using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;

namespace Core.Gambits
{
    
    public class GambitList : MonoBehaviour
    {
        
        [SerializeField]
        private List<Gambit> _gambits = new List<Gambit>();

        [SerializeField]
        private bool _useGambits = true;

        private Character _actor;
        private ActionQueue _actionQueue;

        private void Awake() {

            _actor = GetComponent<Character>();
            _actionQueue = GetComponent<ActionQueue>();

        }

        private void FixedUpdate() {

            if(_useGambits) {
                EvaluateGambits();
            }

        }

        private void EvaluateGambits() {

            for(int i = 0; i < _gambits.Count; i++) {
                
                // Get targets found by gambit's target selector
                Character[] _targets = _gambits[i].target.GetTargets();

                if(_targets != null && _targets.Length != 0) {

                    // Evaluate condition passing the actor and targets
                    bool _condition = _gambits[i].condition.Evaluate(_actor, _targets);

                    // If the condition is met, add the action to the character's queue
                    if(_condition) {

                        // Instantiate scriptable object
                        GameAction _action = Instantiate(_gambits[i].action);

                        _action.actor = _actor;
                        _action.targets = _targets;

                        // Add action to queue
                        _actionQueue.RequestExecution(_action);

                    }

                }

            }

        }

    }

}

