using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousVisualsHandler : MonoBehaviour
{
    public PlayVisuals parent;

    public bool useTimer = true;

    [ShowIf("useTimer")]
    public float time = 1;

    float timer = 0;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(useTimer && timer >= time)
        {
            NextPhase();
            useTimer = false;
        }
    }

    public void NextPhase()
    {
        if(parent != null)
        {
            parent.Next();
        }
    }

    public void DestroyAndEnd()
    {
        if (parent != null)
        {
            parent.Next();
        }
        Destroy(gameObject);
    }
}
