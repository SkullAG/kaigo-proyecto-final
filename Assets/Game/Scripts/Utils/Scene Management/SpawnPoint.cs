using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SpawnPoint : MonoBehaviour {
    
    public string uniqueIdentifier = "";

    public Scene scene => gameObject.scene;

    private void Start() {

        LevelManager.current.spawnPoints.Add(this);

    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {

        Handles.DrawWireDisc(transform.position, Vector3.up, 1);
        Handles.Label(transform.position + Vector3.up * 1.15f, uniqueIdentifier);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);

    }

    #endif

}
