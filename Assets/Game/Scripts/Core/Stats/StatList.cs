using UnityEngine;
using NaughtyAttributes;
using Core.Characters;
using Core.Affinities;
using Core.States;

namespace Core.Stats {

	public class StatList : MonoBehaviour
	{

		[SerializeField] private State deathState;
		
		// Resource stats
		[BoxGroup("Resource")] public Resource healthPoints;
		[BoxGroup("Resource")] public Resource actionPoints;

		// Base stats
		[BoxGroup("Base")] public Bravery bravery;
		[BoxGroup("Base")] public Constitution constitution;
		[BoxGroup("Base")] public Vitality vitality;
		[BoxGroup("Base")] public Determination determination;
		[BoxGroup("Base")] public Agility agility;

		// Affinity stats
		[BoxGroup("Affinities")] public Affinities.Affinities affinity;

		private Character _user;

		private void Awake() {

			_user = GetComponent<Character>();

			healthPoints.value = healthPoints.max;
			actionPoints.value = actionPoints.max;

			vitality.onValueChanged += OnAttributeValueChanged;
			determination.onValueChanged += OnAttributeValueChanged;

			healthPoints.onValueUpdated += OnHealthUpdated;
			actionPoints.onValueUpdated += OnActionPointsUpdated;

			UpdateValues();

		}

		private void OnValidate() {

			if(_user) UpdateValues();

		}

		private void OnAttributeValueChanged(float value) {

			UpdateValues();
			
		}

		public void UpdateValues() {

			healthPoints.max = Mathf.RoundToInt(vitality.CalculateMaximumHealth());
			actionPoints.max = Mathf.RoundToInt(determination.CalculateMaxActionPoints());

			_user.queue.actionSpeed = agility.CalculateActionSpeed();

			bravery.CalculateModifiers();
			constitution.CalculateModifiers();
			vitality.CalculateModifiers();
			determination.CalculateModifiers();
			agility.CalculateModifiers();

		}

		public void OnHealthUpdated(int lastValue, int currentValue) {

			int _d = currentValue - lastValue;

			if(_d < 0) {

				BattleLog.current.WriteLine(string.Format(BattleLogFormats.DAMAGE_RECEIVED, _user.name, Mathf.Abs(_d)));

				if(currentValue <= 0) { // Add death when health is 0
					_user.states.AddState(deathState, Mathf.Infinity, 1);
				}

			} else if (_d > 0) {

				BattleLog.current.WriteLine(string.Format(BattleLogFormats.HEALTH_RECOVERED, _user.name, Mathf.Abs(_d)));

				if (lastValue <= 0) {
					_user.states.RemoveState(deathState);
				}

			}

		}

		public void OnActionPointsUpdated(int lastValue, int currentValue) {

			int _d = currentValue - lastValue;

			// Don't log action points lost :S
			if (_d > 0) {

				BattleLog.current.WriteLine(string.Format(BattleLogFormats.ACTIONP_RECOVERED, _user.name, _d));

			}

		}

	}

}
 
