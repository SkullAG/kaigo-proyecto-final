using UnityEngine;
using NaughtyAttributes;
using Core.States;

public class StatesDebugger : MonoBehaviour
{

    [SerializeField]
    private string _stateID;

    [SerializeField]
    private float _stateDuration;

    [SerializeField]
    private float _power;

    private StateList _stateList;

    private void Awake() {

        _stateList = GetComponent<StateList>();

    }

    [Button]
    private void AddState() {

        _stateList.AddState(_stateID, _stateDuration, _power);

    }

    [Button]
    private void RemoveState() {

        _stateList.RemoveState(_stateID);

    }

}
