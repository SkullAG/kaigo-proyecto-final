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
	public Vector3 offset = Vector3.zero;

	public float degreesPerScreen = 180;

	Camera _camera;
	Mouse _mouse;
	bool _dragging = false;

	Vector2 _lastMousePos = Vector2.zero;

	void Start()
	{
		_camera = GetComponent<Camera>();

		_mouse = Mouse.current;
	}

	private void Update()
	{
		Vector3 actualOffset = transform.localToWorldMatrix.MultiplyVector(offset);
		transform.position = Vector3.Lerp(transform.position, objective.position + actualOffset, acceleration * Time.deltaTime);

		if (_mouse.rightButton.isPressed)
		{
			Vector3 pos = transform.position - actualOffset;
			Vector2 mousePos = _mouse.position.ReadValue() / _camera.pixelRect.size;

			if (!_dragging)
            {
				_lastMousePos = mousePos;
			}
			
			Vector3 delta = mousePos - _lastMousePos;
			Vector3 deltaAngle = new Vector3(delta.y, delta.x,0) * degreesPerScreen;
			//deltaAngle.x = Mathf.Clamp(deltaAngle.x, -90, 90);

			Vector3 finalAngle = deltaAngle + transform.rotation.eulerAngles;
			//finalAngle.x = Mathf.Clamp(finalAngle.x > 180 ? finalAngle.x - 360 : finalAngle.x, -90, 90);
			finalAngle.x = Mathf.Clamp(finalAngle.x, 0, 90);

			_lastMousePos = mousePos;

			//transform.Rotate(new Vector3(delta.y, delta.x, 0f), Space.World);
			transform.rotation = Quaternion.Euler(finalAngle);
			//Debug.Log(finalAngle);

			transform.position = pos + transform.localToWorldMatrix.MultiplyVector(offset);

			_dragging = true;
		}
		else
        {
			_dragging = false;
		}
	}
}
