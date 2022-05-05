using UnityEngine;
using Core.Characters;

namespace Core.States {

	public abstract class Effect : ScriptableObject
	{
		public bool started = false;

		public abstract void Apply(Character actor, float power = 1);
	}

}

