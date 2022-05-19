using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.AI;
using Core.States;
using Core.Characters;

[CreateAssetMenu(fileName = "Target By State", menuName = "Game/AI/Target Filters/Target By State")]
public class TargetByState : TargetFilter
{

    public State[] states;
    public bool notBeingAfflicted = false;

    private HashSet<Character> _characters;

    public void OnEnable() {
        _characters = null;
    }

    public override Character GetTarget(Character actor) {

        // Get valid characters by scope
        _characters = GetValidCharacters(actor, filterScope);

        if(_characters != null) {

            foreach (Character character in _characters) {

                int _afflictionCount = 0;
                
                foreach(State state in states) {

                    if(character.states.IsSufferingState(state)) {

                        _afflictionCount ++;

                    }

                }

                //Debug.Log("Character " + character.name + " is being afflicted by " + _afflictionCount + " states from the list");

                if(notBeingAfflicted) {

                    if(_afflictionCount == 0) {

                        //Debug.Log("Character chosen is " + character.name);

                        return character;

                    }

                } else if (_afflictionCount > 0) {

                    //Debug.Log("Character chosen is " + character.name);

                    return character;

                }

            }



        }

        return null;

    }

    public override void DrawGizmos(Character actor) {}

}
