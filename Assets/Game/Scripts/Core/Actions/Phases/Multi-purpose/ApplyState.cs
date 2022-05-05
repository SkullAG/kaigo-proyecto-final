using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;
using Core.States;
using Core.Affinities;
using System.Linq;

public class ApplyState : ActionPhase
{
	
	private StateAndProbability[] _states;
	private string _deathStateName = "Death";

	private bool _healthDepleted = false;

	public ApplyState(StateAndProbability[] states)
	{

		this._states = states;
	}

	public ApplyState(StateAndDuration[] states) {

		this._states = states.Select(field => new StateAndProbability(field)).ToArray();
	}
	
	// Update phase's processing
	public override void Update() {

		if(started) {

			// End action if there are no states to apply
			if(_states == null || _states.Length == 0) {

				End();
				return;

			}

			if(target != null) {

				// Applies each state
				foreach (var state in _states) {

					foreach (StateAndProbability s in _states)
					{
						Debug.Log(target);
						if (CustomMath.Probability(s.applyStateRawProbability * target.stats.affinity.affinities.GetValue(s.elementType), 1f))
						{
							Debug.Log(s.state.power);
							target.states.AddState(s.state);
						}
					}

					//aaaaa luego lo arreglo bien (tuvo que ser en ese momento)

				}

				// Maybe this shouldn't be here :S
				if(target.stats.healthPoints.depleted) {

					// Add Death state to character
					target.states.AddState(_deathStateName, Mathf.Infinity);

				}

				End();

			}

		}

	}

}
