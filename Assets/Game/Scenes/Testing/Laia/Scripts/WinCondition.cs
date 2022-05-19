using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

public class WinCondition : MonoBehaviour
{
    public GameObject canvasWin;

    private void OnEnable()
    {
        BossSingleton.current.stats.healthPoints.onValueMin += Win;
    }

    private void OnDisable()
    {
        BossSingleton.current.stats.healthPoints.onValueMin -= Win;
    }
    public void Win(int i, int n)
    {
        canvasWin.SetActive(true);
    }
}
