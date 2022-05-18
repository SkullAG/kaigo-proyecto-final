using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.AI;
using Core.Characters;
using Core.Stats;

[CreateAssetMenu(fileName = "Target By Resource", menuName = "Game/AI/Target Filters/Target By Resource")]
public class TargetByResource : TargetFilter
{

    [Range(0, 100)] public float percentage;

    public ComparisonOperator logicOperator;
    public ResourceTypes resourceType;

    private HashSet<Character> _characters;

    public enum ResourceTypes { health, actionPoints }
    public enum ComparisonOperator { lessThan, greaterThan }

    public void OnEnable() {
        _characters = null;
    }
    
    public override Character GetTarget(Character actor) {
        
        // Get valid characters by scope
        _characters = GetValidCharacters(actor, filterScope);

        Character _chosenCharacter = null;
        float _chosenValue = 0;

        if(_characters != null) {

            if(logicOperator == ComparisonOperator.greaterThan) _chosenValue = Mathf.Infinity;

            foreach (Character character in _characters) {

                Resource _resource = null;

                // Select resource depending on enum
                if(resourceType == ResourceTypes.health) _resource = character.stats.healthPoints;
                else if(resourceType == ResourceTypes.actionPoints) _resource = character.stats.actionPoints;

                bool _evaluation = false;
                float _fraction = (_resource.max * percentage) / 100;

                // Select evaluation (< or >) depending on enum
                if(logicOperator == ComparisonOperator.greaterThan) _evaluation = _resource.value >= _fraction;
                else if(logicOperator == ComparisonOperator.lessThan) _evaluation = _resource.value <= _fraction;

                if(_evaluation) {

                    _chosenValue = _resource.value;
                    _chosenCharacter = character;

                }
                
            }

        }

        return _chosenCharacter;

    }

    public override void DrawGizmos(Character actor) {}

}
