using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using System.Collections;
using System.Linq;

public class Targetter : Singleton<Targetter>
{

    public event System.Action<Character> targetSelected = delegate {};
    public event System.Action<Character> targetConfirmed = delegate {};
    public event System.Action<Character> targetCancelled = delegate {};
    public event System.Action<bool> targettingStatusChanged = delegate {};

    [SerializeField] private TargetCursor _cursor;
    [SerializeField] private Character _target;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private GameObject _targetterMessageWindow;

    [SerializeField] private float _range;
    [SerializeField] private float _fieldOfView;
    [SerializeField] private float _startDelay = 0.1f;
    [SerializeField, ReadOnly] private bool _update;

    [SerializeField, ReadOnly] private Vector2[] _viewPositions;
    [SerializeField, ReadOnly] private Character[] _characters;

    [SerializeField] private InputActionReference _navigateAction;
    [SerializeField] private InputActionReference _submitAction;
    [SerializeField] private InputActionReference _cancelAction;

    private VisibilityFilter _filter;

    private bool _pressed = false;

    public Character currentTarget => _target;
    
    private Character _selectedCharacter;

    private void Awake() {

        _filter = GetComponent<VisibilityFilter>();
        _cursor.gameObject.SetActive(false);
        
    }

    private void Start() {

        _filter.onCountUpdated += OnCountUpdated;

    }

    private void OnDisable() {

        _filter.onCountUpdated -= OnCountUpdated;

    }

    private void Update() {

        if(_update) {

            _cameraManager.SetLookPoint(_cursor.transform.position);

            Vector2 _targetViewPos = _filter.camera.WorldToViewportPoint(_target.transform.position);
            Vector2 _control = _navigateAction.action.ReadValue<Vector2>();

            if(_control.magnitude > 0 != _pressed) {

                if(!_pressed) {

                    int _index = UtilityClass.FindClosestPointInDirection(_viewPositions, _targetViewPos, _control, _fieldOfView);
                    Debug.Log($"Pressed directional button ({_control.x}, {_control.y}). Index is: {_index}. ");

                    if(_index != -1) {

                        // Sets the current selected target
                        SelectTarget(_characters[_index]); 

                    }

                }

                _pressed = _control.magnitude > 0;

            } 

            if(_submitAction.action.triggered && _target != null) {

                // Target is confirmed
                targetConfirmed(_target);
                Disable();

            } else if (_cancelAction.action.triggered) {
                
                // Target is cancelled
                targetCancelled(_target);
                Disable();

            }

        }

    }
    
    [Button]
    public void Enable() => StartCoroutine(_Enable());

    private IEnumerator _Enable() {

        // Delay the activation to avoid conflicts with UI inputs
        yield return new WaitForSeconds(_startDelay);

        _cursor.gameObject.SetActive(true);

        _filter.enabled = true;
        _update = true;

        _selectedCharacter = PartyManager.current.GetSelectedCharacter();
        _characters = FindVisibleCharacters();

        if(_characters.Length > 0) {

            // Sort targets based on distance
            _characters.OrderBy(x => Vector3.Distance(x.transform.position, _selectedCharacter.transform.position));
            
            // Select first target (closest one)
            SelectTarget(_characters[0]);

        }

        //_cameraManager.SetLookPoint(_cursor.transform.position);

        _cameraManager.rotationEnabled = false;
        _targetterMessageWindow.SetActive(true);
        _selectedCharacter.navBody.isParalized = true;

        targettingStatusChanged(true);

    }

    [Button]
    public void Disable() {

        _cursor.target = null;
        _cursor.gameObject.SetActive(false);

        _filter.enabled = false;
        _update = false;

        _cameraManager.RemoveLookPoint();
        _cameraManager.rotationEnabled = true;
        _targetterMessageWindow.SetActive(false);
        _selectedCharacter.navBody.isParalized = false;

        targettingStatusChanged(false);

    }

    private void SelectTarget(Character character) {

        _target = character;
        _cursor.target = _target.transform;

        targetSelected(_target);

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
            float _sqrDistance = (_selectedCharacter.transform.position - checker.transform.position).sqrMagnitude;

            if( _sqrDistance < _range * _range ) {

                _characters.Add(checker.GetComponent<Character>());

            }
            
        }

        return _characters.ToArray();

    }

}
