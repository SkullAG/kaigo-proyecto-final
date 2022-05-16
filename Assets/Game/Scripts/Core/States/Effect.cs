using UnityEngine;
using Core.Characters;
using System.Collections.Generic;

namespace Core.States {

	public abstract class Effect : ScriptableObject
	{
		public bool started = false;

		public GameObject visualEffect;
		private ParticleSystem particles;

		public virtual void Apply(Character actor, ref Dictionary<Effect, GameObject> instancedVisuals, float power = 1)
        {
		
			ManageVisuals(actor, ref instancedVisuals);

		}

		public virtual void OnEffectActivated(Character actor) { }
		public virtual void OnEffectExpired(Character actor) { }

		public void ManageVisuals(Character actor, ref Dictionary<Effect, GameObject> instantiedVisuals)
        {
			//Debug.Log(visualsStarted);
			if (!instantiedVisuals.ContainsKey(this) && visualEffect)
			{
				Debug.Log("poison");

				particles = Instantiate(visualEffect, actor.transform).GetComponent<ParticleSystem>();

				Collider col = actor.GetComponent<Collider>();

				particles.transform.localRotation = Quaternion.identity;
				particles.transform.localPosition = col.bounds.center - particles.transform.position;

				var sh = particles.shape;
				sh.scale = col.bounds.size;

				//visualEffect.transform.localScale = actor.GetComponent<Collider>().bounds.size;

				instantiedVisuals.Add( this, particles.gameObject);
			}
			else if (instantiedVisuals.ContainsKey(this) && !instantiedVisuals[this].activeInHierarchy)
			{
				instantiedVisuals[this].SetActive(true);

			}
		}
	}

}

