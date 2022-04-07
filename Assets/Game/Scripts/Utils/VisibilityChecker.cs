using UnityEngine;
using UnityEditor;

public class VisibilityChecker : MonoBehaviour
{

    private Renderer[] _renderers;

    private void Awake() {

        _renderers = GetComponentsInChildren<Renderer>();
 
    }
    
    public bool IsVisible(Plane[] planes) {

        for(int i = 0; i < _renderers.Length; i++) {

            if(GeometryUtility.TestPlanesAABB(planes, _renderers[i].bounds)) {

                return true;

            }

        }

        return false;

    }

    #if UNITY_EDITOR

    private void OnDrawGizmos() {

        //Handles.Label( transform.position, isVisible ? "Visible" : "Invisible" );
        
    }

    #endif

}
