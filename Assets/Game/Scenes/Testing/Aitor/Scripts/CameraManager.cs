using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
	public Transform objective;
	public float acceleration;
	public Vector3 offset = Vector3.zero;

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, objective.position + offset, acceleration * Time.deltaTime);
	}
}
