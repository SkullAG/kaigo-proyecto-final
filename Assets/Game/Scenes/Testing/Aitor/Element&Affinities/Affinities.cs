using Core.Characters;
using NaughtyAttributes;
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
		public const float None = 0;
		public float Physical = 0;
		public float Magical = 0;
		public float Fire = 0;
		public float Thunder = 0;
		public float Ice = 0;
		public float Wind = 0;
		public float Void = 0;
		public float Poison = 0;
		public float Bleeding = 0;

		public static string[] affinityNames { get { return typeof(AffinityList).GetFields().Select(field => field.Name).ToArray(); } }
		public static Type[] affinityTypes { get { return typeof(AffinityList).GetFields().Select(field => field.GetType()).ToArray(); } }

		public static DropdownList<Type> affinityDropDown { get {
				if(_savedDPL == null || _savedDPL.Count() != affinityNames.Length)
				{
					_savedDPL = adpl();
				}
				return _savedDPL;
					} }

		private static DropdownList<Type> _savedDPL = null; 
		private static DropdownList<Type> adpl()
        {
			string[] an = affinityNames;
			Type[] at = affinityTypes;
			DropdownList<Type> dpl = new DropdownList<Type>();

			for (int i = 0; i < an.Length; i++)
            {
				//new DropdownAttribute("a");
				dpl.Add(an[i], at[i]);
            }
			return dpl;
		}

		public float GetRawValue(string valueName)
        {
			return (float)GetType().GetField(valueName).GetValue(this);
        }

		public float GetValue(string valueName)
		{
			return (-(float)GetType().GetField(valueName).GetValue(this) + 1);
		}

		public AffinityList Normalize()
        {
			AffinityList outVal = new AffinityList();
			float total = 0;

			foreach (FieldInfo p in typeof(AffinityList).GetFields())
			{
				total += (float)p.GetValue(this);
			}

			return outVal.Mult(1 / total);
		}
		public AffinityList Mult(float factor)
        {
			AffinityList outVal = new AffinityList();

			foreach (FieldInfo p in typeof(AffinityList).GetFields())
			{
				if (!p.IsLiteral)
                {
					float val = (float)p.GetValue(this);
					p.SetValue(outVal, val * factor);
					
					if(val != 0)
					//Debug.Log(p.Name + " = " + val * factor);
                }
			}

			return outVal;
		}
	}

	[Serializable]
	public class Affinities
	{
		public AffinityList affinities = new AffinityList();

		public static float CalculateDamageDealt(Character target, AffinityList elementalDamage)
        {
			return target.stats.constitution.DamageMultiplier * target.stats.affinity.DamageCalculation(elementalDamage);

		}

		public float DamageCalculation(AffinityList damages)
		{
			float dmg = 0;

			foreach (FieldInfo p in typeof(AffinityList).GetFields())
			{
				//Debug.Log((float)p.GetValue(damages) + " * " + (-(float)p.GetValue(affinities) + 1) + " = " + ((float)p.GetValue(damages) * (-(float)p.GetValue(affinities) + 1)));
				dmg += (float)p.GetValue(damages) * (-(float)p.GetValue(affinities) + 1);
			}

			return dmg;
		}
	}
}

