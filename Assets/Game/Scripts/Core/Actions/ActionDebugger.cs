using UnityEngine;
using Core.Actions;
using NaughtyAttributes;

public class ActionDebugger : MonoBehaviour
{

    [SerializeField, Expandable]
    private GameAction _action;

    private void Awake() {

        // To execute an action from a ScriptableObject, instantiate one first
        // instead of using the asset 
        _action = Instantiate(_action);

    }

    [Button]
    private void Test() {
        
        _action.Execute();

    }

    private void Update() {
        
        if(_action != null) {

            if(_action.isRunning) {
                _action.Update();
            }

        }

    }

}
