using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavSistem : MonoBehaviour
{
	public Transform Objective;

	public float radius;
	public float height;

	public Vector3 pivot;

	public NavMeshAreas areaMask;
	public NavMeshAgents agentType;

	public float velocity = 10;

	public bool blockRotation;

	[NaughtyAttributes.HideIf("blockRotation")]
	public float rotationVelocity = 25;

	public NavMeshQueryFilter filter;

	float agentRadius;

	NavMeshPath path;
	bool hasPath;

	[Flags]
	public enum NavMeshAreas
	{
		None = 0,
		Walkable = 1, NotWalkable = 2, Jump = 4, //es tecnicamente binario
		All = ~0,
	}

	public enum NavMeshAgents
	{
		Humanoid = 0,
		BigThing = 1, //es tecnicamente cada agente, pero no hay forma de actualizarlo en tiempo de edicion :c
	}


	void Start()
	{
		filter.areaMask = (int)areaMask;
		filter.agentTypeID = NavMesh.GetSettingsByIndex((int)agentType).agentTypeID;

		agentRadius = NavMesh.GetSettingsByIndex((int)agentType).agentRadius;
		CalculatePath();
	}

	private void FixedUpdate()
	{
		CalculatePath();

		if(hasPath)
		{
			//Esto es para los saltitos que se niega a detectar así que me los tengo que inventar.
			/*if (path.corners.Length > 2 && Vector3.Distance(path.corners[1], path.corners[2]) <= agentRadius)
			{
				Debug.Log("jump");
			}*/
			if(!blockRotation)
            {
				Vector3 dir = new Vector3(path.corners[1].x - transform.position.x, 0 , path.corners[1].z - transform.position.z).normalized;
				float targetAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;



				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotationVelocity);
			}

			MoveTowards(path.corners[1]);
		}
	}

	public void MoveTowards(Vector3 position)
	{
		Vector3 dir = new Vector3(position.x - (transform.position.x + pivot.x), position.y - (transform.position.y + pivot.y), position.z - (transform.position.z + pivot.z));

		Vector3 mov = dir.normalized * velocity * Time.fixedDeltaTime;

		//Debug.Log("Min(" + mov + ", " + dir + ") = " + Vector3.Min(mov, dir));

		mov = CustomMath.CloseTo0(mov, dir);

		transform.position += mov;
	}

	public void CalculatePath()
	{
		path = new NavMeshPath();
		hasPath = NavMesh.CalculatePath((transform.position + pivot), Objective.position, filter, path);

		for (int i = 0; i < path.corners.Length-1; i++)
		{
			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);
		}
	}

	private void OnDrawGizmos()
	{
		if(height < radius*2)
		{
			height = radius*2;
		}

		Gizmos.DrawSphere(transform.position + pivot, 0.05f);
		//CustomGizmos.DrawWireCapsule( transform.position + pivot, radius, height);
	}

		/*{
			pivot = Handles.FreeMoveHandle(pivot, Quaternion.identity, 1, Vector3.zero * 0.5f, Handles.ArrowHandleCap);
		}*/
}
