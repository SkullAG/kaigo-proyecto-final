using UnityEngine;
using Core.States;
using Core.Characters;
using Core.Affinities;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Resource Over Time Effect", menuName = "Game/States/Effects/Resource Over Time")]
public class ResourceOverTime : Effect
{
	public GameObject visualEffect;
	private ParticleSystem particles;

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

	private bool visualsStarted = false;

	private float _timer;

	private void OnEnable() {

		_timer = 0;
		visualsStarted = false;

	}

	public override void Apply(Character actor, float power) {
		
		//Debug.Log(visualsStarted);
		if (!visualsStarted && visualEffect)
		{
			Debug.Log("poison");

			particles = Instantiate(visualEffect, actor.transform).GetComponent<ParticleSystem>();

			Collider col = actor.GetComponent<Collider>();

			particles.transform.localRotation = Quaternion.identity;
			particles.transform.localPosition = col.bounds.center - particles.transform.position;

			var sh = particles.shape;
			sh.scale = col.bounds.size;

			//visualEffect.transform.localScale = actor.GetComponent<Collider>().bounds.size;

			visualsStarted = true;
		}

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
