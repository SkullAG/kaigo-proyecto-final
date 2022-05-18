using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

public class CoinsDrop : MonoBehaviour
{
    private Character _character;
    private Inventory _inventory;
    public GameObject _object;

    public int coinEnemy = 10;
    private void OnEnable()
    {
        _character.stats.healthPoints.onValueMin += drop;
    }

    private void OnDisable()
    {
        _character.stats.healthPoints.onValueMin -= drop;
    }

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    public void drop(int p, int f)
    {
        _inventory.coins = _inventory.coins + coinEnemy;
        Instantiate(_object, transform.position, Quaternion.identity);
    }
}
