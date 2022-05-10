using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NavBodyAnimatorManager : MonoBehaviour
{
    NavBodySistem agent;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavBodySistem>();

        if (!animator) animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("movementFactor", agent.movementFactor);
        animator.SetFloat("rotationFactor", agent.rotationFactor);
    }
}
