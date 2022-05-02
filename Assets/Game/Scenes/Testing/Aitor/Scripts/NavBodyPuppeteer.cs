using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PlayerInput))]
public class NavBodyPuppeteer : MonoBehaviour
{
	public LayerMask layerMask;
	
	public NavBodySistem character { get { return _character; } set { if(_character) _character.isBeingControled = false; _character = value; } }
	[SerializeField]
	NavBodySistem _character;
	Camera _camera;
	PlayerInput _input;
	InputAction _moveAction;
	Vector2 _move;
	Mouse _mouse;
	EventSystem _eventSystem;

	// Start is called before the first frame update
	void Start()
	{
		_camera = Camera.main;
		_input = GetComponent<PlayerInput>();
		_moveAction = _input.actions.FindAction("Move");
		_mouse = Mouse.current;
		_eventSystem = EventSystem.current;

	}

	// Update is called once per frame
	void Update()
	{
		if (!_character) return;
		_character.isBeingControled = true;
		_move = _moveAction.ReadValue<Vector2>();

		if (_move != Vector2.zero && !_character.isFalling && !_character.isJumping)
		{
			Vector3 dir = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0) * new Vector3(_move.x, 0, _move.y);
			//character.MoveTowardsPoint(character.transform.position + dir, true);
			_character.ObjectivePoint = _character.transform.position + dir*0.1f;
			//character.ObjectivePoint = character.transform.position;
		}

		if (_mouse.leftButton.isPressed && !_eventSystem.IsPointerOverGameObject())
		{
			Vector3 mousePos = _camera.ScreenToWorldPoint((Vector3)_mouse.position.ReadValue() + Vector3.forward);

			Vector3 dir = (mousePos - _camera.transform.position).normalized;

			RaycastHit hit;
			if(Physics.Raycast(_camera.transform.position, dir, out hit, _camera.farClipPlane * 2, layerMask))
            {
				_character.ObjectivePoint = hit.point;
			}
		}
	}

}
