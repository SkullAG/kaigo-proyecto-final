using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Resource Percentage Condition", menuName = "Game/AI/Conditions/Resource Percentage")]
public class ResourcePercentageCondition : BehaviourCondition
{

    private enum ResourceTypes { health, actionPoints }
    private enum ComparisonOperator { lessThan, greaterThan }

    [SerializeField]
    private ResourceTypes _resourceToEvaluate;

    [SerializeField]
    private ComparisonOperator _operator;

    [SerializeField]
    private int _percentage;

    public override bool Evaluate(Character actor, Character[] targets) {
        
        int _value = 0;
        int _max = 0;
        float _p = 0;

        if(_resourceToEvaluate == ResourceTypes.health) {

            _value = actor.stats.healthPoints.value;
            _max = actor.stats.healthPoints.max;

        } else {

            _value = actor.stats.actionPoints.value;
            _max = actor.stats.actionPoints.max;

        }

        _p = (float)_value / (float)_max;

        switch(_operator) {

            case ComparisonOperator.greaterThan:
                return _p > (float)_percentage / 100;

            case ComparisonOperator.lessThan:
                return _p < (float)_percentage / 100;

        }

        return false;

    }

}
