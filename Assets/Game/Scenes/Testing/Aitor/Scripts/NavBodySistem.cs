using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NavBodySistem : MonoBehaviour
{
	public bool UsePosition = false;
	[HideIf("UsePosition")]
	public Transform Objective;
	[ShowIf("UsePosition")]
	public Vector3 ObjectivePoint = Vector3.zero;

	//public float radius;
	//public float height;

	public Vector3 pivot;

	public NavMeshAreas areaMask;
	public NavMeshAgents agentType;

	public float velocity = 10;
	public float visualJumpHeight = 1;
	public float linkCompletionTime = 1;

	public bool blockRotation;

	[HideIf("blockRotation")]
	[Min(0)]
	public float rotationVelocity = 180;
	[HideIf("blockRotation")]
	[Range(0, 360)]
	public float fieldOfView = 90;

	public bool turnBeforeJump = true;

	public NavMeshQueryFilter filter;

	float agentRadius;

	NavMeshPath path;
	bool hasPath;

	public NavMeshLink link { get; private set; }

	[HideInInspector]
	public bool isBeingControled = false;
	public bool isJumping { get; private set; }
	public bool isParalized;
	public bool isFalling { get; private set; }
	public NavMeshHit ground { get; private set; }

	[ReadOnly]
	public float movementFactor = 0;

	[ReadOnly]
	public float rotationFactor = 0;

	float dowVel = 0;

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
		ObjectivePoint = transform.position;

		filter.areaMask = (int)areaMask;
		filter.agentTypeID = NavMesh.GetSettingsByIndex((int)agentType).agentTypeID;

		agentRadius = NavMesh.GetSettingsByIndex((int)agentType).agentRadius;
		CalculatePath();
	}

	void CheckForGround()
	{

		NavMeshHit hit;

		isFalling = !NavMesh.SamplePosition(transform.position + pivot + Vector3.up * (agentRadius-0.05f), out hit, agentRadius, filter.areaMask);

		ground = hit;
	}

	private void Update()
	{
		movementFactor = 0;
		rotationFactor = 0;

		CheckForGround();

		if (!isFalling && !isJumping && !isParalized)
		{
			CalculatePath();

			dowVel = 0;

			if (hasPath && path.corners.Length > 1)
			{
				if (!blockRotation)
				{
					Vector3 dir = new Vector3(path.corners[1].x - transform.position.x, 0, path.corners[1].z - transform.position.z).normalized;
					
					float _angle = RotateTowards(dir);

					//Debug.Log(transform.rotation.eulerAngles.y + ", " + targetAngle);
					if (Mathf.Abs(_angle) <= fieldOfView / 2)
					{
						MoveTowardsPoint(path.corners[1]);
					}

				}
				else
				{
					MoveTowardsPoint(path.corners[1]);
				}

				calculateLinks();
			}
		}
        else if (isFalling)
		{
			dowVel -= Physics.gravity.y * Time.deltaTime;
			transform.position -= Vector3.up * dowVel * Time.deltaTime;

			//Debug.Log("aire");
		}
	}

	public float RotateTowards(Vector3 dir) {

		float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

		float deltaAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), Mathf.Min(Mathf.Abs(deltaAngle), rotationVelocity * Time.deltaTime));

		rotationFactor = Mathf.Clamp(deltaAngle / (rotationVelocity * Time.deltaTime), -1, 1);

		return Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle);

	}

    void calculateLinks()
	{
		NavMeshLinksManager.NavMeshLinkSides linkSide = 0;

		link = NavMeshLinksManager.PointIsOnLink(transform.position + pivot, filter.agentTypeID, out linkSide);

		if (link)
		{
			//if (NavMeshLinksManager.PointIsOnCertainLink(path.corners[2], link, (linkSide == NavMeshLinksManager.NavMeshLinkSides.end) ? NavMeshLinksManager.NavMeshLinkSides.start : NavMeshLinksManager.NavMeshLinkSides.end)/* || path.corners[2].y < (transform.position + pivot).y*/)
			if (path.corners.Length > 2 && NavMeshLinksManager.LineTravesingCertainLink(path.corners[1], path.corners[2], link)/* || path.corners[2].y < (transform.position + pivot).y*/)
			{
				StartCoroutine(Jump(transform.position + pivot, NavMeshLinksManager.GetOppositePointOnLink(transform.position + pivot, link, linkSide)));
				//Debug.Log("jump dettected, FINALLY!");
			}
		}
	}

	public void MoveTowardsPoint(Vector3 position, bool correctWithNavMesh = true)
	{
		Vector3 dir = position - (transform.position + pivot);

		Vector3 mov = dir.normalized * velocity * Time.deltaTime;

		//Debug.Log("Min(" + mov + ", " + dir + ") = " + Vector3.Min(mov, dir));

		mov = CustomMath.CloseTo0(mov, dir);

		movementFactor = mov.magnitude / (velocity * Time.deltaTime);

		if (correctWithNavMesh)
        {
			//Debug.Log("a");
			Debug.DrawRay(transform.position + pivot + mov, Vector3.up, Color.blue);

			NavMeshHit hit;
			if(NavMesh.SamplePosition(transform.position + pivot + mov, out hit, agentRadius, filter.areaMask))
            {
				mov = hit.position - (transform.position + pivot);
            }
        }

		transform.position += mov;
	}

	public void CalculatePath()
	{
		if (!UsePosition && !isBeingControled && Objective)
		{
			ObjectivePoint = Objective.position;
		}

		path = new NavMeshPath();
		NavMeshHit hit;
		bool detected = NavMesh.SamplePosition(ObjectivePoint, out hit, 10, filter.areaMask);
		hasPath = NavMesh.CalculatePath((transform.position + pivot), detected ? hit.position : ObjectivePoint, filter, path);

		for (int i = 0; i < path.corners.Length-1; i++)
		{
			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.green);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position + pivot, 0.05f);
		//CustomGizmos.DrawWireCapsule( transform.position + pivot, radius, height);
	}

	IEnumerator Jump(Vector3 initialPos, Vector3 finalPos)
	{
		isJumping = true;

		float factor = 0;
		float dist = Vector2.Distance(new Vector2(initialPos.x, initialPos.z), new Vector2(finalPos.x, finalPos.z));
		float highestH = Mathf.Max(initialPos.y, finalPos.y);
		float timer = 0;


		Vector3 dir = new Vector3(finalPos.x - initialPos.x, 0, finalPos.z - initialPos.z).normalized;
		float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

		//Debug.Log(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle));
		//Debug.Log(transform.rotation.eulerAngles.y + ", " + targetAngle);
		while (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle)) > 0 && turnBeforeJump)
		{

			RotateTowards(dir);

			yield return new WaitForFixedUpdate();
		}

		Vector3 midPole = new Vector3((initialPos.x + finalPos.x) / 2, (visualJumpHeight + highestH), (initialPos.z + finalPos.z) / 2);

		while (transform.position + pivot != finalPos)
		{
			factor += timer / linkCompletionTime;
			//factor += (velocity * Time.deltaTime) / dist;

			timer += Time.fixedDeltaTime;

			dist -= (velocity * Time.fixedDeltaTime);

			factor = Mathf.Clamp01(factor);

			transform.position = CustomMath.onePoleBezierLerp(initialPos, finalPos, midPole, factor) - pivot;

			yield return new WaitForFixedUpdate();
		}

		isJumping = false;
	}
}
