using UnityEngine;
using UnityEditor;

public class VisibilityChecker : MonoBehaviour
{
    
    public bool isVisible;

    private void OnBecameVisible() {
        isVisible = true;
    }

    private void OnBecameInvisible() {
        isVisible = false;
    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {

        //Handles.Label( transform.position, isVisible ? "Visible" : "Invisible" );
        
    }

    #endif

}
