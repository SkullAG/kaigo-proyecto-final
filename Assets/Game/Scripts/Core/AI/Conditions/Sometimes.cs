using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu( fileName = "Condition By Frequency", menuName = "Game/AI/Conditions/By Frequency" )]
public class ConditionByFrequency : BehaviourCondition {
    
    [Range(0, 100)]
    public float frequency;

    public override bool Evaluate(Character actor, Character targets) {
        
        return Random.Range(0, 100) < frequency;
        
    }

}
