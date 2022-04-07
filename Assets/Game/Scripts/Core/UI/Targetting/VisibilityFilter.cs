using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class VisibilityFilter : MonoBehaviour
{
    
    public System.Action<int> onCountUpdated = delegate {};

    public new Camera camera;

    private VisibilityChecker[] _checkers;
    private int _lastCheckerCount;

    // Use hashset for performance reasons
    public HashSet<VisibilityChecker> visibleObjects = new HashSet<VisibilityChecker>();

    private void Awake() {

        _checkers = FindObjectsOfType<VisibilityChecker>();

    }

    private void Update() {

        Plane[] _planes = GeometryUtility.CalculateFrustumPlanes(camera);

        // Add/remove checkers if visible/not
        for (int i = 0; i < _checkers.Length; i++) {
            
            if(_checkers[i].IsVisible(_planes)) {

                visibleObjects.Add(_checkers[i]);

                onCountUpdated(visibleObjects.Count);

            } else {

                visibleObjects.Remove(_checkers[i]);

                onCountUpdated(visibleObjects.Count);

            }

        }

    }

}
