using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CameraManager : MonoBehaviour
{
	public Transform objective;

	public float acceleration;

	public Vector3 transformedRotOffset { get { return Matrix4x4.Rotate(positionRotation).MultiplyVector(rotatedOffset); } private set { } }
	
	[SerializeField]
	private Vector3 _rotatedOffset = new Vector3(0,0,-9);
	public Vector3 rotatedOffset { get { return _rotatedOffset * Mathf.Min(zoomFactor, 1-collisionCorrection); } }

	[Range(0, 1)]
	public float minZoomFactor = 0.3f;

	[Range(0, 1)]
	public float zoomFactor = 1;

	private float collisionCorrection = 0;

	public Vector3 offset = Vector3.zero;

	public float sensitivity = 1;
	public float zoomSensitivity = 1f;

	public float mouseSentivityMultiplier = 0.5f;

	public LayerMask collisionMask;

	public Transform secondObjective;

	public float autoRotationVelocity = 8;

	Camera _camera;

	PlayerInput _input;
	InputAction _lookInput;
	InputAction _zoomInput;
	InputAction _RightClick;

	Quaternion positionRotation;

	public bool hasLookPoint = false;
	public Vector3 lookPoint = Vector3.zero;

	//Vector2 _lastMousePos = Vector2.zero;

	void Start()
	{
		_camera = GetComponent<Camera>();

		_input = InputSingleton.current.input;

		_lookInput = _input.actions.FindAction("Look");
		_zoomInput = _input.actions.FindAction("Zoom");
		_RightClick = _input.actions.FindAction("RightClick");

		//_mouse = Mouse.current;
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, objective.position + transformedRotOffset + offset, acceleration * Time.deltaTime);

		Vector2 deltaLook = _lookInput.ReadValue<Vector2>();
		if(deltaLook != Vector2.zero)
        {
			bool rc = true;
			if (_lookInput.activeControl.device is Mouse)
			{
				rc = _RightClick.ReadValue<float>() == 1;

				deltaLook *= mouseSentivityMultiplier;
			}

			if (rc)
			{
				Vector3 deltaAngle = new Vector3(deltaLook.y, deltaLook.x, 0) * sensitivity;

				Vector3 finalAngle = deltaAngle + positionRotation.eulerAngles;

				finalAngle.x = Mathf.Clamp(finalAngle.x, 0, 90);

				Vector3 actualOffset = transformedRotOffset;

				positionRotation = Quaternion.Euler(finalAngle);

				transform.position = transform.position - actualOffset + transformedRotOffset;

			}

        }

		float zoom = Mathf.Clamp(_zoomInput.ReadValue<float>(),-10, 10) * zoomSensitivity * Time.deltaTime;

		zoomFactor = Mathf.Clamp(zoomFactor - zoom, minZoomFactor, 1);

		RaycastHit hit;
		if(Physics.Raycast(objective.position + offset, transform.position - (objective.position + offset), out hit, _rotatedOffset.magnitude, collisionMask))
        {
			Debug.DrawLine(objective.position + offset, (transform.position - (objective.position + offset)).normalized * hit.distance);
			collisionCorrection = Mathf.Clamp01(1 - (hit.distance / _rotatedOffset.magnitude));

		}
		else
        {
			collisionCorrection = 0;

		}

		if(hasLookPoint)
        {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookPoint - transform.position, Vector3.up), autoRotationVelocity * Time.deltaTime);
        }
        else
        {
			transform.rotation = positionRotation;
		}
	}

	public void SetLookPoint(Vector3 point)
    {
		lookPoint = point;
		hasLookPoint = true;
	}

	public void RemoveLookPoint()
	{
		hasLookPoint = false;
	}
}
