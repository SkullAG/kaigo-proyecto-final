using UnityEngine;
using NaughtyAttributes;
using Core.States;

public class StatesDebugger : MonoBehaviour
{

    [SerializeField]
    private string _stateID;
    
    private StateList _stateList;

    private void Awake() {

        _stateList = GetComponent<StateList>();

    }

    [Button]
    private void AddState() {

        _stateList.AddState(_stateID);

    }

    [Button]
    private void RemoveState() {

        _stateList.RemoveState(_stateID);

    }

}
