using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

public class WinCondition : MonoBehaviour
{
    public GameObject canvasWin;
    public Character character;

    private void OnEnable()
    {
        character.stats.healthPoints.onValueMin += Win;
    }

    private void OnDisable()
    {
        character.stats.healthPoints.onValueMin -= Win;
    }
    public void Win(int i, int n)
    {
        canvasWin.SetActive(true);
    }
}
