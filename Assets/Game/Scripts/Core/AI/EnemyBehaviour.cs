using UnityEngine;
using UnityEditor;

using Core.Characters;
using Core.Gambits;

using NaughtyAttributes;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _detectionAngle;
    [SerializeField] private LayerMask _detectionMask;
    [SerializeField] private QueryTriggerInteraction _queryTriggers;
    [SerializeField] private float _timeToForget = 5;

    [SerializeField, ReadOnly] private bool _elementInRange;
    [SerializeField, ReadOnly] private bool _elementInView;
    [SerializeField, ReadOnly] private bool _isAIEnabled;

    private Character _enemy;
    private GambitList _gambits;
    private NavBodySistem _navBody;

    private Collider[] _colliders;

    private float _timer;
    private Vector3 _originalPosition;

    private void Awake() {

        _enemy = GetComponent<Character>();
        _gambits = _enemy.gambits;
        _navBody = _enemy.navBody;

        _originalPosition = transform.position;

        _timer = _timeToForget;

        _gambits.SetEnabled(false);

    }

    private void Update() {

        _elementInRange = PerformAreaDetection();

        // If element in range and in view
        if(_elementInRange) {

            if(!_isAIEnabled) {

                // Look if elements are inside field of view
                _elementInView = PerformAngleDetection(); 

                if(_elementInView) {

                    //Debug.Log("Enabling enemy gambits");

                    _timer = 0;

                    // Enable AI
                    _gambits.SetEnabled(true);
                    _isAIEnabled = true;

                }

            }

        } else {

            _elementInView = false;

            if(_timer >= _timeToForget) {

                // AI is disabled when enemy forgets
                _gambits.SetEnabled(false);
                _isAIEnabled = false;

                // Enemy returns to its original position
                _navBody.ObjectivePoint = _originalPosition; 

            } else {

                _timer += Time.deltaTime;

            }

        }

    }

    private bool PerformAreaDetection() {

        // Check for colliders in range
        _colliders = Physics.OverlapSphere(_originalPosition, _detectionRadius, _detectionMask, _queryTriggers);

        if(_colliders != null) {

            if(_colliders.Length > 0) {

                return true;

            }

        }

        return false;

    }

    private bool PerformAngleDetection() {

        if(_colliders != null) {

            foreach (Collider c in _colliders) {

                Vector3 _dir = (c.transform.position - transform.position).normalized;

                //Debug.DrawRay(transform.position, _dir, Color.blue);
                //Debug.DrawRay(transform.position, transform.forward, Color.yellow);

                float _angle = Mathf.Abs(Vector3.Angle(_dir, transform.forward));

                //Debug.Log("Angle: " + _angle);

                if(_angle < _detectionAngle / 2) {

                    //Debug.Log("Player is seen");
                    //Debug.Break();

                    return true;

                }
                
            }

        }

        //Debug.Log("Player not is seen");

        return false;

    }

    #if UNITY_EDITOR

    private void OnDrawGizmosSelected() {

        Vector3 _position = Vector3.zero;

        if(Application.isPlaying) {

            _position = _originalPosition;

            Handles.DrawDottedLine(_position, transform.position, 0.75f);

        } else {

            _position = transform.position;

        }

        Handles.DrawWireDisc(_position, Vector3.up, _detectionRadius);

        Vector3 _right = transform.forward, _left = transform.forward;

        _right = Quaternion.Euler(0, _detectionAngle / 2, 0) * _right;
        _left = Quaternion.Euler(0, -(_detectionAngle / 2), 0) * _left;

        Gizmos.DrawRay(transform.position, _right * _detectionRadius);
        Gizmos.DrawRay(transform.position, _left * _detectionRadius);

        if(_colliders != null && !_isAIEnabled) {
            
            foreach (var c in _colliders) {
                Gizmos.DrawLine(_originalPosition, c.transform.position);
                //Handles.Label(c.transform.position, "DETECTED");
            }

        }

    }

    #endif

}
