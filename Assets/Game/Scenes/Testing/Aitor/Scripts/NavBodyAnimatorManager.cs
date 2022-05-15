using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavBodyAnimatorManager : MonoBehaviour
{
    NavBodySistem agent;
    public Animator animator;

    public float movementFactorDelta = 1;
    public float rotationFactorDelta = 1;

    float _movementFactor;
    float _rotationFactor;

    float _startupTime;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavBodySistem>();

        if (!animator) animator = GetComponent<Animator>();

        _startupTime = Time.realtimeSinceStartup;

    }

    // Update is called once per frame
    void Update() {

        _movementFactor = Mathf.MoveTowards(_movementFactor, agent.movementFactor, movementFactorDelta * Time.deltaTime);
        _rotationFactor = Mathf.MoveTowards(_rotationFactor, agent.rotationFactor, rotationFactorDelta * Time.deltaTime);;

        animator.SetFloat("movementFactor", _movementFactor);
        animator.SetFloat("rotationFactor", _rotationFactor);
        
    }
}
