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

	public Vector3 transformedRotOffset { get { return transform.localToWorldMatrix.MultiplyVector(rotationOffset); } private set { } }
	
	public Vector3 rotationOffset = Vector3.zero;
	public Vector3 offset = Vector3.zero;

	public float sensitivity = 1;

	public float mouseSentivityMultiplier = 0.5f;

	Camera _camera;

	PlayerInput _input;
	InputAction _lookInput;
	InputAction _RightClick;

	//Vector2 _lastMousePos = Vector2.zero;

	void Start()
	{
		_camera = GetComponent<Camera>();

		_input = InputSingleton.current.input;

		_lookInput = _input.actions.FindAction("Look");
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

				Vector3 finalAngle = deltaAngle + transform.rotation.eulerAngles;

				finalAngle.x = Mathf.Clamp(finalAngle.x, 0, 90);

				Vector3 actualOffset = transformedRotOffset;

				transform.rotation = Quaternion.Euler(finalAngle);

				transform.position = transform.position - actualOffset + transformedRotOffset;

			}

        }

	}

}
