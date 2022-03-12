using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGizmos
{
	public static Color cyan = new Color(0, 1, 1, 1);
	public static void DrawWireCapsule(Vector3 position, float radius, float height, Color? color = null)
	{
		Gizmos.color = color ?? Color.cyan;

		Vector3 SphereOff = Vector3.up * (height / 2 - radius);

		Gizmos.DrawWireSphere(position + SphereOff, radius);
		Gizmos.DrawWireSphere(position - SphereOff, radius);
		Gizmos.DrawLine(position + Vector3.forward * radius + SphereOff, position + Vector3.forward * radius - SphereOff);
		Gizmos.DrawLine(position + Vector3.back * radius + SphereOff, position + Vector3.back * radius - SphereOff);
		Gizmos.DrawLine(position + Vector3.right * radius + SphereOff, position + Vector3.right * radius - SphereOff);
		Gizmos.DrawLine(position + Vector3.left * radius + SphereOff, position + Vector3.left * radius - SphereOff);
	}
}
