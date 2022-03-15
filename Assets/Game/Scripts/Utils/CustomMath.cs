using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class CustomMath
{
	
	static Vector3 TempVec3 = Vector3.zero;

    ///<summary>
    ///	Is true if float a and b have the same, if one of the two is 0 will return false
    ///	</summary>
    ///	<param name="a">Float or int</param>
    ///	<param name="b">Float or int</param>
    public static bool SignesAreEqual(float a, float b)
	{
		bool result = ((a < 0 && b < 0) || (a > 0 && b > 0));
		return result;
	}

	public static float MaxAbs(float a, float b)
	{
		float result = (Mathf.Abs(a) > Mathf.Abs(b)) ? a : b;
		return result;
	}

	///<summary>
	///	returns 1 or -1 if the given vaue is 0 returns 0
	///	</summary>
	///	<param name="a">Float or int</param>
	public static float ExtractSign(float a)
	{
		if (a == 0) return 0;
		float result = a / Mathf.Abs(a);
		return result;
	}

	///<summary>
	///	Interpolates the velocity usin acceleration and friction
	///	</summary>
	///	<param name="limitVelocity">The maximum velocity or the desired velocity, default is infinity</param>
	public static float CalculateVelocity(float currentVelocity, float acceleration, float limitVelocity = Mathf.Infinity, float friction = 1f)
    {
		return Mathf.Lerp(currentVelocity, limitVelocity, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(currentVelocity - limitVelocity), 1));
	}


	///<summary>
	///	returns true if b is between a +- range
	///	default range is 0.001
	///	</summary>
	///	<param name="a">Float or int</param>
	///	<param name="b">Float or int</param>
	///	<param name="range">Float or int, is the radius of the circle range</param>
	public static bool Aproximately(float a, float b, float range = 0.001f)
	{
		bool result = ((a + range > b) && (a - range < b));
		return result;
	}

	public static bool AproximatelyVector2(Vector2 a, Vector2 b, float range = 0.001f)
	{
		bool resultX = ((a.x + range > b.x) && (a.x - range < b.x));
		bool resultY = ((a.y + range > b.y) && (a.y - range < b.y));
		return (resultX && resultY);
	}

	public static bool AproximatelyVector3(Vector3 a, Vector3 b, float range = 0.001f)
	{
		bool resultX = ((a.x + range > b.x) && (a.x - range < b.x));
		bool resultY = ((a.y + range > b.y) && (a.y - range < b.y));
		bool resultZ = ((a.z + range > b.z) && (a.z - range < b.z));
		return (resultX && resultY && resultZ);
	}

	public static float Hypotenuse(float legA, float legB)
	{
		float result = Mathf.Sqrt(Mathf.Pow(legA, 2) + Mathf.Pow(legB, 2));
		return result;
	}

	public static float Round(float a)
	{
		float diference = Mathf.Abs(a) % 1;
		float result = (diference > 0.5f) ? 1 - diference : -diference;
		result = a + result * ExtractSign(a);
		return result;
	}

	public static Vector2 RoundV2(Vector2 a)
	{
		Vector2 result = new Vector2(Round(a.x), Round(a.y));
		return result;
	}

	public static float CloseTo0(float a, float b)
	{
		return (Mathf.Abs(a) < Mathf.Abs(b)) ? a : b;
	}
	public static Vector2 CloseTo0(Vector2 a, Vector2 b)
	{
		return (a.magnitude < b.magnitude) ? a : b;
	}
	public static Vector3 CloseTo0(Vector3 a, Vector3 b)
	{
		return (a.magnitude < b.magnitude) ? a : b;
	}
	public static float FarFrom0(float a, float b)
	{
		return (Mathf.Abs(a) > Mathf.Abs(b)) ? a : b;
	}
	public static Vector2 FarFrom0(Vector2 a, Vector2 b)
	{
		return (a.magnitude > b.magnitude) ? a : b;
	}
	public static Vector3 FarFrom0(Vector3 a, Vector3 b)
	{
		return (a.magnitude > b.magnitude) ? a : b;
	}

	public static T GetCopyOf<T>(this T comp, T other) where T : Component
	{
		Type type = comp.GetType();
		Type otherType = other.GetType();
		if (type != otherType)
		{
			Debug.LogError($"The type \"{type.AssemblyQualifiedName}\" of \"{comp}\" does not match the type \"{otherType.AssemblyQualifiedName}\" of \"{other}\"!");
			return null;
		}

		BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
		PropertyInfo[] pinfos = type.GetProperties(flags);

		foreach (var pinfo in pinfos)
		{
			if (pinfo.CanWrite)
			{
				try
				{
					pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
				}
				catch
				{
				}
			}
		}

		FieldInfo[] finfos = type.GetFields(flags);

		foreach (var finfo in finfos)
		{
			finfo.SetValue(comp, finfo.GetValue(other));
		}
		return comp as T;
	}

	public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
	{
		return go.AddComponent<T>().GetCopyOf(toAdd) as T;
	}

	/*public static T OverlapCircleAll<T>(Vector2 point, float radius,int layerMask = Physics.DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity) where T : Collider2D
    {
		Collider2D[] collides = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);
        for(int i = 0; i < collides.Length; i++)
		{
			Debug.Log(collides[i].GetType());
		}
		

		return null;
		//return colliders as T;
	}*/

}
