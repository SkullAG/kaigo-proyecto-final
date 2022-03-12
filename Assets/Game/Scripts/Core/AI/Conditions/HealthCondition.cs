using UnityEngine;
using Core.AI;
using Core.Characters;

public class HealthCondition : BehaviourCondition
{

    private enum ComparisonOperator { less, greater }

    [SerializeField]
    private ComparisonOperator _operator;

    [SerializeField]
    private int _percentage;

    public override bool Evaluate(Character actor, Character[] targets) {
        
        int _p = actor.stats.healthPoints.value / actor.stats.healthPoints.max;

        switch(_operator) {

            case ComparisonOperator.greater:
                return _p >= _percentage / 100;

            case ComparisonOperator.less:
                return _p <= _percentage / 100;

        }

        return false;

    }

}
