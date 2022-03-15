using UnityEngine;
using System.Collections.Generic;
using Core.Actions;

public class ActionList : MonoBehaviour
{
    
    [SerializeField]
    private List<GameAction> _actions = new List<GameAction>();

    private List<GameAction> _runtimeActions = new List<GameAction>();

    private void Awake() {

        InstantiateAll();

    }

    private void OnDisable() {

        DestroyAll();

    }

    private void InstantiateAll() {

        for(int i = 0; i < _actions.Count; i++) {

            _actions[i] = Instantiate(_actions[i]);

            for(int j = 0; j < _actions[i].phases.Count; j++) {

                _actions[i].phases[j] = Instantiate(_actions[i].phases[j]);

            }

        }

    }

    private void DestroyAll() {

        for(int i = 0; i < _actions.Count; i++) {

            for(int j = 0; j < _actions[i].phases.Count; j++) {

                Destroy( _actions[i].phases[j] );

            }

            Destroy( _actions[i] );

        }

    }

    private GameAction GetAction(string id) {

        for(int i = 0; i < _actions.Count; i++) {

            if(_actions[i].id == id) {
                return _actions[i];
            }

        }

        return null;

    }

}
