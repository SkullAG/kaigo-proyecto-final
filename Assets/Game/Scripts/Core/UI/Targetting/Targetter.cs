using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using System.Linq;

public class Targetter : MonoBehaviour
{

    [SerializeField] private PlayerInput _input;
    [SerializeField] private TargetCursor _cursor;
    [SerializeField] private Character _target;

    [SerializeField] private float _range;
    [SerializeField, ReadOnly] private bool _update;

    [SerializeField] private Vector2[] _viewPositions;
    [SerializeField] private Character[] _characters;

    private InputAction _navigateAction;
    private InputAction _submitAction;

    private VisibilityFilter _filter;


    private void Awake() {

        _filter = GetComponent<VisibilityFilter>();
        _cursor.gameObject.SetActive(false);

        _navigateAction = _input.actions.FindAction("Navigate", true);

    }

    private void OnEnable() {

        _filter.onCountUpdated += OnCountUpdated;

    }

    private void OnDisable() {

        _filter.onCountUpdated -= OnCountUpdated;

    }

    private void Update() {

        if(_update) {

            Vector2 _cursorViewPosition = _filter.camera.WorldToViewportPoint(_cursor.transform.position);
            Vector2 _control = _navigateAction.ReadValue<Vector2>();

            if(_control.magnitude > 0) {

                Debug.Log("Pressing directional buttons...");

                int _index = UtilityClass.FindClosestPointInDirection(_viewPositions, _cursorViewPosition, _control);

                if(_index != -1) {

                    SetTarget(_characters[_index]);
                    Debug.Log("Target is now: " + _characters[_index]);   

                }

            }

        }

    }
    
    [Button]
    public void Enable() {

        _cursor.gameObject.SetActive(true);

        _filter.enabled = true;
        _update = true;

        _characters = FindVisibleCharacters();

        if(_characters.Length > 0) {
            
            // Select first target
            SetTarget(_characters[0]);

        }

    }

    [Button]
    public void Disable() {

        _cursor.target = null;
        _cursor.gameObject.SetActive(false);

        _filter.enabled = false;
        _update = true;

    }

    private void SetTarget(Character character) {

        _target = character;
        _cursor.target = _target.transform;

    }

    private void OnCountUpdated(int count) {

        _characters = FindVisibleCharacters();

        // Get positions of characters in screen coordinates
        _viewPositions = new Vector2[_characters.Length];

        for (int i = 0; i < _viewPositions.Length; i++) {

            _viewPositions[i] = _filter.camera.WorldToViewportPoint(_characters[i].transform.position);

        }

    }

    private Character[] FindVisibleCharacters() {

        List<Character> _characters = new List<Character>();

        foreach(VisibilityChecker checker in _filter.visibleObjects) {

            // Performant way of measuring distance
            float _sqrDistance = (transform.position - checker.transform.position).sqrMagnitude;

            if( _sqrDistance < _range * _range ) {

                _characters.Add(checker.GetComponent<Character>());

            }
            
        }

        return _characters.ToArray();

    }

}
