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

        int _count = 0;

        if(_characters != null) {

            foreach (Character character in _characters) {
                
                foreach(State state in states) {

                    bool _afflicted = character.states.IsSufferingState(state);

                    if(!notBeingAfflicted) {

                        if(_afflicted) return character;

                    } else {

                        if(!_afflicted) {
                            _count++;
                        }

                        if(_count == states.Length) return character;

                    }

                }

            }

        }

        return null;

    }

    public override void DrawGizmos(Character actor) {}

}
