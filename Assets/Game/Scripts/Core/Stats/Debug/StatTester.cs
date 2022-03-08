using UnityEngine;
using Core.Characters;
using NaughtyAttributes;

public class StatTester : MonoBehaviour
{
    
    private Character _character;

    private void Awake() {
        _character = GetComponent<Character>();
    }

    [Button]
    private void AddHealth() {

        _character.stats.healthPoints.value += 10;

    }

    [Button]
    private void RemoveHealth() {

        _character.stats.healthPoints.value -= 10;

    }

    [Button]
    private void AddActionPoints() {

        _character.stats.actionPoints.value += 10;

    }

    [Button]
    private void RemoveActionPoints() {

        _character.stats.actionPoints.value -= 10;

    }

}
