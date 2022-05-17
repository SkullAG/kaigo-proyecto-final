using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class AccessPoint : MonoBehaviour
{

    public string spawnPointIdentifier = "EMPTY_ID";

    private void Awake() {

        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;

    }
    
    private void OnTriggerEnter(Collider other) {

        LevelManager.current.Teleport(gameObject.scene, spawnPointIdentifier);

    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {

        Handles.Label(transform.position + Vector3.up * 1.15f, "to: " + spawnPointIdentifier);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);

    }

    #endif

}
