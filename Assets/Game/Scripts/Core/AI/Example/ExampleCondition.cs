using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Example Behaviour Condition", menuName = "Game/AI/Example/Behaviour Condition")]
public class ExampleCondition : BehaviourCondition
{

    // Abstract method that returns the bool value for the condition
    public override bool Evaluate(Character actor, Character target) {
        
        // Returns true if actor health is half its maximum health
        bool _condition = actor.stats.healthPoints.value <= actor.stats.healthPoints.max / 2;

        return _condition;

    }

}
