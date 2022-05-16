using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using NaughtyAttributes;
using UnityEngine.Events;
using Core.Affinities;
using System;

namespace Core.States {
	[System.Serializable]
	public class StateAndDuration
    {
		
		public StateAndDuration(State _state, float _duration, float _power = 1)
        {
			state = _state;
			duration = _duration;
			power = _power;
		}

		public State state;
		public float duration;
		public float power = 1;
	}

	[Serializable]
	public class StateAndProbability
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="inState"></param>
		/// <param name="inProbability"></param>
		/// <param name="inElementName"></param>
		public StateAndProbability(StateAndDuration inState, float inProbability = 100, string inElementName = "none")
		{
			state = inState;
			elementType = inElementName;
			_Probability = inProbability;
		}
		public StateAndDuration state;

		private string[] _affinityNames => AffinityList.affinityNames;
		[Dropdown("_affinityNames"), SerializeField] 
		public string elementType;

		[Range(0, 100), SerializeField]
		private float _Probability = 100;
		public float applyStateRawProbability { get { return _Probability / 100; } }
	}

	[CreateAssetMenu(fileName = "State", menuName = "Game/States/State")]
	public class State : ScriptableObject
	{

		public string displayName;

		[System.Serializable]
		public class InstanceValues
		{
			public InstanceValues(float _duration, float _power)
			{
				duration = _duration;
				power = _power;
			}

			public float duration = 0;
			public float power = 0;
		}

		public enum StateValuesBlendMode { none, overwrite,
			pickMaxTime, pickMaxPower,
			maxTimeAndUnifyPower, addTimeAndUnifyPower, 
			maxBoth, 
			addTime, addPower, 
			addBoth
		}

		[SerializeField]
		private List<Effect> _effects = new List<Effect>();

		[NonSerialized]
		public Dictionary<Effect, GameObject> instancedVisuals = new Dictionary<Effect, GameObject>();

		public bool allowMultipleInstances = false;

		[HideIf("allowMultipleInstances")]
		public StateValuesBlendMode blendMode = StateValuesBlendMode.none;

		private InstanceValues mainInstance = new InstanceValues(0, 1);

		private List<InstanceValues> instances = new List<InstanceValues>();

		[SerializeField]
		private string _id = "InNeedOfId";

		public string id { get { return _id; } }

		public void StartState(Character actor, float duration, float power = 1)
		{

			//Debug.Log("La duracion es de: " + duration);

			foreach(Effect effect in _effects) {
				effect.OnEffectActivated(actor);
			}

			if(allowMultipleInstances)
			{
				instances.Add(new InstanceValues(duration, power));
			}
			else if(mainInstance.duration <= 0)
			{
				mainInstance.duration = duration;
				mainInstance.power = power;
			}
			else
			{
				BlendValues(duration, power);
			}
			
		}

		public void EndState(Character actor) {

			foreach(Effect effect in _effects) {
				effect.OnEffectExpired(actor);
			}

		}

		public void BlendValues(float duration, float power)
		{
			float dur;

			switch (blendMode)
			{
				case StateValuesBlendMode.none:
					return;

				case StateValuesBlendMode.overwrite:
					mainInstance.duration = duration;
					mainInstance.power = power;
					return;

				case StateValuesBlendMode.pickMaxTime:

					if(duration ==Mathf.Max(mainInstance.duration, duration))
                    {
						mainInstance.duration = duration;
						mainInstance.power = power;
                    }
					return;

				case StateValuesBlendMode.pickMaxPower:
					if (duration == Mathf.Max(mainInstance.duration, duration))
					{
						mainInstance.duration = duration;
						mainInstance.power = power;
					}
					return;

				case StateValuesBlendMode.maxTimeAndUnifyPower:
					dur = Mathf.Max(mainInstance.duration, duration);
					mainInstance.power = (mainInstance.duration * mainInstance.power + duration * power) / dur;
					mainInstance.duration = dur;
					return;

				case StateValuesBlendMode.addTimeAndUnifyPower:
					dur = mainInstance.duration + duration;
					mainInstance.power = (mainInstance.duration * mainInstance.power + duration * power) / dur;
					mainInstance.duration = dur;
					return;

				case StateValuesBlendMode.maxBoth:
					mainInstance.duration = Mathf.Max(mainInstance.duration, duration);
					mainInstance.power = Mathf.Max(mainInstance.power, power);
					return;

				case StateValuesBlendMode.addTime:
					mainInstance.duration += duration;
					return;

				case StateValuesBlendMode.addPower:
					mainInstance.power += power;
					return;

				case StateValuesBlendMode.addBoth:
					mainInstance.duration += duration;
					mainInstance.power += power;
					return;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="actor"></param>
		/// <returns>true if it's the last frame of the action</returns>
		public bool Affect(Character actor)
		{

			//Debug.Log("Entra");
			if(allowMultipleInstances)
			{
				for (int i = 0; i < _effects.Count; i++)
				{
					float resultPow = 0;

					for(int e = instances.Count-1; e >= 0 ; e--)
					{
						instances[e].duration -= Time.deltaTime;

						resultPow += instances[e].power;

						if (instances[e].duration <= 0)
						{
							instances.RemoveAt(e);
						}
					}
					
					_effects[i].Apply(actor, ref instancedVisuals, resultPow);

					if (instancedVisuals.ContainsKey(_effects[i]))
					{
						instancedVisuals[_effects[i]].SetActive(instances.Count > 0);

					}
				}

				return !(instances.Count > 0);
			}
			else
			{

				//Debug.Log("Duration is " + mainInstance.duration + "...");

				mainInstance.duration -= Time.deltaTime;
				for (int i = 0; i < _effects.Count; i++)
				{

					_effects[i].Apply(actor, ref instancedVisuals, mainInstance.power);

					if (instancedVisuals.ContainsKey(_effects[i]))
					{
						instancedVisuals[_effects[i]].SetActive(mainInstance.duration > 0);

					}
					

				}
				return mainInstance.duration <= 0;
			}
		}

	}
}

