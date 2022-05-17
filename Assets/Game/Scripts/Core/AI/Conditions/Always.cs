using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Always", menuName = "Game/AI/Conditions/Always")]
public class Always : BehaviourCondition
{

    public override bool Evaluate(Character actor, Character targets)
    {
        return true;
    }

}
