using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using Core.Actions;
using NaughtyAttributes;

namespace Core.Gambits
{
    
    public class GambitList : MonoBehaviour
    {

        [Expandable]
        public GambitSet set;

        [SerializeField]
        private bool _enabled = true;

        [SerializeField]
        private bool _freeUse = false;

        private Character _actor;
        private ActionQueue _actionQueue;
        private ActionList _actionList;

        private void Awake() {

            _actor = GetComponent<Character>();
            _actionQueue = GetComponent<ActionQueue>();
            _actionList = GetComponent<ActionList>();

        }

        private void FixedUpdate() {

            if(_enabled) {

                EvaluateGambits();

            }

        }

        public void SetEnabled(bool enabled) {

            _enabled = enabled;

        }

        private void EvaluateGambits() {

            for(int i = 0; i < set.list.Count; i++) {
                
                Character _target = set.list[i].target?.GetTarget(_actor);

                bool _condition = _target != null ? set.list[i].condition.Evaluate(_actor, _target) : false;

                // If action list has the gambit's action
                if(_condition) {

                    _actionQueue.RequestExecution(set.list[i].actionReference, _actor, _target, _freeUse);

                    break;

                }

            }

        }  

        #if UNITY_EDITOR

        private void OnDrawGizmos() {

            if(set) {

                for (int i = 0; i < set.list.Count; i++) {
                        
                    set.list[i].target.DrawGizmos(_actor);

                }

            }

        }

        #endif

    }

}

