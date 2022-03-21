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
                
                Character[] _targets = _gambits[i].target?.GetTargets();

                if(_targets != null && _targets.Length != 0) {

                    bool _condition = _gambits[i].condition.Evaluate(_actor, _targets);

                    if(_condition) {

                        GameAction _action = _actionList.GetAction(_gambits[i].action.id);

                        if(_action != null) {

                            _action.actor = _actor;
                            _action.targets = _targets;

                            _actionQueue.RequestExecution(_action);

                            break;

                        }

                    }

                }

            }

        }  

        public void ShiftGambit(int shift, Gambit gambit) {

            if( _gambits.Contains(gambit) ) {

                int _index = _gambits.IndexOf(gambit);
                int _shiftedIndex = _index + shift;

                _shiftedIndex = Mathf.Clamp(_shiftedIndex, 0, _gambits.Count - 1);

                Gambit _temp = _gambits[_shiftedIndex];

                _gambits[_shiftedIndex] = _gambits[_index];
                _gambits[_index] = _temp;

            }

        }

        public void AddGambit(Gambit gambit) {

            _gambits.Add(gambit);

        }

        public void RemoveGambit(Gambit gambit) {

            _gambits.Remove(gambit);

        }

    }

}

