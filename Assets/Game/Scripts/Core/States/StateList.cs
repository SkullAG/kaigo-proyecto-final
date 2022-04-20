using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.States {

    public class StateList : MonoBehaviour
    {
        
        [SerializeField]
        private List<State> _availableStates = new List<State>();

        [SerializeField, NonReorderable, ReadOnly]
        private List<State> _currentStates = new List<State>();

        private Character _actor;

        private void Awake() {

            _actor = GetComponent<Character>();

        }

        private void OnEnable() {

            for(int i = 0; i < _availableStates.Count; i++) {

                if(_availableStates[i] != null) {

                    _availableStates[i] = Instantiate(_availableStates[i]);

                }

            }

        }

        private void OnDisable() {

            for(int i = 0; i < _availableStates.Count; i++) {
                Destroy(_availableStates[i]);
            }

        }

        private void Update() {

            for(int i = 0; i < _currentStates.Count; i++) {
                _currentStates[i].Affect(_actor);
            }

        }

        public void AddState(string id) {

            State _state = GetState(id);

            if(_state != null) {
                
                if(!_currentStates.Contains(_state) && _availableStates.Contains(_state)) {

                    _currentStates.Add(_state);

                }

            }

        }

        public void RemoveState(string id) {

            State _state = GetState(id);

            if(_state != null) {
                _currentStates.Remove(_state);
            }

        }

        public State GetState(string id) {

            for(int i = 0; i < _availableStates.Count; i++) {

                if(_availableStates[i].id == id) {

                    return _availableStates[i];

                }

            }

            return null;

        }

    }

}

