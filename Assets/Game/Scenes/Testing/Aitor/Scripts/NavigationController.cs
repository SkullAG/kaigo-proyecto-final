using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour
{

	public Transform objective;

	NavMeshAgent navAgent;
	// Start is called before the first frame update
	void Start()
	{
		navAgent = GetComponent<NavMeshAgent>();
		//
		navAgent.SetDestination(objective.position);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		navAgent.SetDestination(objective.position);
		//NavMesh.CalculatePath();

		for (int i = 0; i < navAgent.path.corners.Length - 1; i++)
		{
			Debug.DrawLine(navAgent.path.corners[i], navAgent.path.corners[i + 1], Color.red);
		}
	}
}
