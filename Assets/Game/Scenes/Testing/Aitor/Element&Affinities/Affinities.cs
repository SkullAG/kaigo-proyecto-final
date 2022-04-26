using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Affinities
{
	[Serializable]
	public class AffinityList
	{
		public float Physical = 0;
		public float Magical = 0;
		public float Fire = 0;
		public float Thunder = 0;
		public float Ice = 0;
		public float Wind = 0;
		public float Void = 0;
		public float Poison = 0;
		public float Bleeding = 0;
	}
	public class Affinities : MonoBehaviour
	{
		public AffinityList affinities = new AffinityList();

		public float DamageCalculation(AffinityList damages)
		{
			float dmg = 0;

			foreach (FieldInfo p in typeof(AffinityList).GetFields())
			{
				dmg += (float)p.GetValue(damages) * (-(float)p.GetValue(affinities) + 1);
			}

			return dmg;
		}
	}
}

