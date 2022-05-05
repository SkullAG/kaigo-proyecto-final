using UnityEngine;
using Core.States;
using Core.Characters;
using Core.Affinities;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Resource Over Time Effect", menuName = "Game/States/Effects/Resource Over Time")]
public class ResourceOverTime : Effect
{
	public enum ResourceType { HP, AP }
	public enum AffectionType { plainValue, elemental}

	public ResourceType affectedResource;
	public AffectionType affectionType;

	public float timeBetweenTicks = 1;

	[Tooltip("It gets multiplied by the power parameter.\nIf there isn't power it gets used directly.")]
	public int valuePerTick = 1;

	[ShowIf("affectionType", AffectionType.elemental)]
	public bool normalizeElements = false;
	[ShowIf("affectionType", AffectionType.elemental)]
	[Tooltip("The product o the multiplication between Power and Value gets transformed to its elemental values")]
	public AffinityList elementMultiplicator;

	

	private float _timer;

	private void OnEnable() {

		_timer = 0;

	}

	public override void Apply(Character actor, ref Dictionary<Effect, GameObject> instancedVisuals, float power) {

		base.Apply(actor, ref instancedVisuals, power);

		_timer += Time.deltaTime; 

		if( _timer >= timeBetweenTicks ) {

			_timer = 0;
			
			switch(affectedResource, affectionType) {

				case (ResourceType.HP, AffectionType.elemental):
					actor.stats.healthPoints.value += Mathf.RoundToInt(Affinities.CalculateDamageDealt( actor, normalizeElements ? elementMultiplicator.Normalize().Mult(valuePerTick * power) : elementMultiplicator.Mult(valuePerTick * power)));
					break;

				case (ResourceType.HP, AffectionType.plainValue):
					actor.stats.healthPoints.value += Mathf.RoundToInt(valuePerTick * power);
					break;

				case (ResourceType.AP, AffectionType.elemental):
					actor.stats.actionPoints.value += Mathf.RoundToInt(Affinities.CalculateDamageDealt( actor, normalizeElements ? elementMultiplicator.Normalize().Mult(valuePerTick * power) : elementMultiplicator.Mult(valuePerTick * power)));
					break;

				case (ResourceType.AP, AffectionType.plainValue):
					actor.stats.actionPoints.value += Mathf.RoundToInt(valuePerTick * power);
					break;

			}

		}

	}
}
