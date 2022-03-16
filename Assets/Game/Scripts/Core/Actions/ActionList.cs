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

                for(int j = 0; j < _actions[i].phases.Count; j++) {

                    _runtimeActions[i].phases[j] = Instantiate(_actions[i].phases[j]);

                }

            }

        }

        private void DestroyAll() {

            for(int i = 0; i < _runtimeActions.Length; i++) {

                for(int j = 0; j < _runtimeActions[i].phases.Count; j++) {

                    Destroy( _runtimeActions[i].phases[j] );

                }

                Destroy( _runtimeActions[i] );

            }

        }

        public GameAction GetAction(string id) {

            for(int i = 0; i < _runtimeActions.Length; i++) {

                if(_runtimeActions[i].id == id) {
                    return _runtimeActions[i];
                }

            }

            return null;

        }

    }
    
}

