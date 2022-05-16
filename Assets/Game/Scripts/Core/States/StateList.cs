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

					//AddState(_availableStates[i], 10, 1);

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

				if(_currentStates[i].Affect(_actor))
				{
					RemoveState(_currentStates[i]);
				}
			}

		}

		public void AddState(string id, float duration, float power = 1) { // pueeeeeees, los a valores se suman?

			State _state = GetState(id);

			if(_state != null) {

				if (!_currentStates.Contains(_state) && _availableStates.Contains(_state)) {

					BattleLog.current.WriteLine(string.Format(BattleLogFormats.STATUS_AFFLICTION, _actor.name, _state.displayName));

					_state.StartState(_actor, duration, power);
					_currentStates.Add(_state);

					Debug.Log("State added");

				}

			}

		}

		public void AddState(State baseStateSO, float duration, float power = 1) { //Aitor: y no hace falat mas, le mandas el SO plano inicial y te lo pilla, de nada.
			AddState(baseStateSO.id, duration, power);
		}

		public void AddState(StateAndDuration state)
		{
			AddState(state.state.id, state.duration, state.power);
		}

		public void RemoveState(string id) {

			Debug.Log("State removed");

			State _state = GetState(id);

			if(_state != null) {

				BattleLog.current.WriteLine(string.Format(BattleLogFormats.STATE_DISPELLED, _state.displayName, _actor.name));

				_state.EndState(_actor);
				_currentStates.Remove(_state);

			}

		}

		public void RemoveState(State baseStateSO){

			RemoveState(baseStateSO.id);

		}

		public State GetState(string id) {

			for(int i = 0; i < _availableStates.Count; i++) {

				if(_availableStates[i].id == id) {

					return _availableStates[i];

				}

			}

			return null;

		}

		public bool IsSufferingState(State state) {

			return _currentStates.Contains(state);

		}

	}

}

