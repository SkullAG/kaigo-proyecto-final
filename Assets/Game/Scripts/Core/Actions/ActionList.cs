using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

namespace Core.Actions
{

    public class ActionList : MonoBehaviour
    {
        
        [SerializeField]
        private List<GameAction> _actions = new List<GameAction>();

        [SerializeField, NonReorderable, ReadOnly, Expandable]
        private GameAction[] _runtimeActions;

        public GameAction[] runtimeActions => _runtimeActions;
        public GameAction[] actions => _actions.ToArray();

        private void Awake() {

            InstantiateAll();

        }

        private void OnDisable() {

            DestroyAll();

        }

        private void InstantiateAll() {

            _runtimeActions = new GameAction[_actions.Count];

            for(int i = 0; i < _actions.Count; i++) {
                _runtimeActions[i] = Instantiate(_actions[i]);
            }

        }

        private void DestroyAll() {

            for(int i = 0; i < _runtimeActions.Length; i++) {
                Destroy( _runtimeActions[i] );
            }

        }

        public GameAction GetAction(string name) {

            for(int i = 0; i < _runtimeActions.Length; i++) {

                if(_runtimeActions[i].displayName == name) {
                    return _runtimeActions[i];
                }

            }

            return null;

        }

        public GameAction GetAction(int index) {

            for(int i = 0; i < _runtimeActions.Length; i++) {

                if(i == index) {
                    return _runtimeActions[i];
                }

            }

            return null;

        }

    }
    
}

