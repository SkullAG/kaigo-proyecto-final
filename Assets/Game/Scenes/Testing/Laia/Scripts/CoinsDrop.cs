using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

public class CoinsDrop : MonoBehaviour
{
    private Character _character;
    private Inventory _inventory;

    public int coinEnemy;
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

    public void drop(int f)
    {
        _inventory.coins = _inventory.coins + coinEnemy;
    }
}
