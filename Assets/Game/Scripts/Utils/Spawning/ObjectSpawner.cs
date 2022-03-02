using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectSpawner : MonoBehaviour
{
      
    public event System.Action<Transform> onSpawn = delegate {};

    [SerializeField]
    private PoolConfig _config;

    [SerializeField]
    private float _time;

    [SerializeField]
    private bool _update = true;

    private float _timer;
    private Pool _pool;

    private void Start() {

        _pool = PoolManager.current.GetPool(_config);

    }

    private void Update() {

        if(_update) {

            _timer += Time.deltaTime;

            if(_timer >= _time) {
                _timer = 0;
                Spawn();
            }

        }
        
    }

    public Transform Spawn() {

        Transform _object = _pool.GetObject(transform.position, Vector3.one, 0, true);

        onSpawn(_object);

        return _object;

    }

    public void SetRespawnTime(float time) {

        _time = time;

    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {

        //Handles.DrawWireDisc(transform.position, Vector3.forward, 0.25f);
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        Handles.Label(transform.position, _timer.ToString());

    }

    #endif

}
