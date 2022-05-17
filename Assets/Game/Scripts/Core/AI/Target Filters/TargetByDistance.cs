using System.Collections.Generic;
using Core.AI;
using Core.Characters;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Target By Distance", menuName = "Game/AI/Target Filters/Target By Distance")]
public class TargetByDistance : TargetFilter
{
    
    public Mode mode; 

    public enum Mode { closer, farthest } // sorry, no numbers :'(

    private HashSet<Character> _characters;

    public void OnEnable() {
        _characters = null;
    }

    public override Character GetTarget(Character actor) {

        // Get valid characters by scope
        _characters = GetValidCharacters(actor, filterScope);

        if(_characters != null) {

            //Debug.Log("Valid characters: " + _characters.ToString());

            Character _chosenCharacter = null;
            
            float _distanceEvaluated = 0;

            if(mode == Mode.closer) _distanceEvaluated = Mathf.Infinity;

            foreach (Character character in _characters) {

                //if(character == actor && excludeSelf) continue; // Don't evaluate distance with self lul
                
                float _distance = (character.transform.position - actor.transform.position).sqrMagnitude;

                bool _evaluation = false;

                if(mode == Mode.closer) { 

                    // look for closest character
                    _evaluation = _distance < _distanceEvaluated * _distanceEvaluated;

                } else if (mode == Mode.farthest) {
                    
                    // look for farthest character
                    _evaluation = _distance > _distanceEvaluated * _distanceEvaluated;

                }

                // If evaluation results true,
                // return chosen character
                if(_evaluation) {

                    _distanceEvaluated = _distance;
                    _chosenCharacter = character;

                }

                

            }

            Debug.DrawLine(actor.transform.position, _chosenCharacter.transform.position, Color.red);

            return _chosenCharacter;

        }

        return null;

    }

    #if UNITY_EDITOR
    public override void DrawGizmos(Character actor) {

        /*if(_characters != null) {

            foreach(Character character in _characters) {

                Handles.Label(character.transform.position, character.isAlly ? "ALLY" : "ENEMY");

            }

        }*/

    }
    #endif

}
