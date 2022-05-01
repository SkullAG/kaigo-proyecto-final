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
        private ActionList _actionList;

        public List<Gambit> list => _gambits;

        private void Awake() {

            _actor = GetComponent<Character>();
            _actionQueue = GetComponent<ActionQueue>();
            _actionList = GetComponent<ActionList>();

        }

        private void FixedUpdate() {

            if(_useGambits) {

                EvaluateGambits();

            }

        }

        private void EvaluateGambits() {

            for(int i = 0; i < _gambits.Count; i++) {
                
                Character _target = _gambits[i].target?.GetTarget(_actor);

                bool _condition = _target != null ? _gambits[i].condition.Evaluate(_actor, _target) : false;

                GameAction _action = _actionList.GetAction(_gambits[i].action.displayName);

                if(_action != null) {

                    if(_condition) {

                        _actionQueue.RequestExecution(_action.displayName, _actor, _target);

                        break;

                    }

                }

            }

        }  

        public void ShiftGambit(int shift, Gambit gambit) {

            if( _gambits.Contains(gambit) ) {

                int _currentIndex = _gambits.IndexOf(gambit);
                int _nextIndex = Mathf.Clamp(_currentIndex + shift, 0, _gambits.Count - 1);

                //Debug.Log("Swapping gambit " + _currentIndex + " with " + _nextIndex);

                var _temp = _gambits[_nextIndex];

                _gambits[_nextIndex] = _gambits[_currentIndex];
                _gambits[_currentIndex] = _temp;

            }

        }

        public void AddGambit(Gambit gambit) {

            _gambits.Add(gambit);

        }

        public void RemoveGambit(Gambit gambit) {

            _gambits.Remove(gambit);

        }

        #if UNITY_EDITOR

        private void OnDrawGizmos() {

            if(_gambits != null) {

                if( _gambits.Count > 0 ) {

                    for (int i = 0; i < _gambits.Count; i++) {
                        
                        _gambits[i].target.DrawGizmos(_actor);

                    }

                }

            }

        }

        #endif

    }

}

