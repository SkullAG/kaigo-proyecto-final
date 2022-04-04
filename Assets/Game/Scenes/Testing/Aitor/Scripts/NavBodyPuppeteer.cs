using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PlayerInput))]
public class NavBodyPuppeteer : MonoBehaviour
{
	public NavBodySistem character;
	Camera _camera;
	PlayerInput _input;
	InputAction _moveAction;
	Vector2 _move;
	Mouse _mouse;


	// Start is called before the first frame update
	void Start()
	{
		_camera = Camera.main;
		_input = GetComponent<PlayerInput>();
		_moveAction = _input.actions.FindAction("Move");

		_mouse = Mouse.current;
	}

	// Update is called once per frame
	void Update()
	{
		character.isBeingControled = true;
		_move = _moveAction.ReadValue<Vector2>();

		if (_move != Vector2.zero && !character.isFalling && !character.isJumping)
		{
			Vector3 dir = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * new Vector3(_move.x, 0, _move.y);
			//character.MoveTowardsPoint(character.transform.position + dir, true);
			character.ObjectivePoint = character.transform.position + dir*0.1f;
			//character.ObjectivePoint = character.transform.position;
		}

		if (_mouse.leftButton.isPressed)
		{
			Vector3 mousePos = _camera.ScreenToWorldPoint((Vector3)_mouse.position.ReadValue() + Vector3.forward);

			Vector3 dir = (mousePos - _camera.transform.position).normalized;

			RaycastHit hit;
			if(Physics.Raycast(_camera.transform.position, dir, out hit, _camera.farClipPlane * 2))
            {
				character.ObjectivePoint = hit.point;
			}
		}
	}
}
