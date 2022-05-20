using System.Collections.Generic;
using Core.AI;
using Core.Characters;
using UnityEngine;
using System.Linq;

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

        if(_characters == null) return null;

        List<Character> _sortedCharacters = _characters.ToList();

        _sortedCharacters = _sortedCharacters
            .OrderBy( x => (x.transform.position - actor.transform.position).sqrMagnitude )
            .ToList();

        if(_sortedCharacters.Count >= 1)
        {
            if(mode == Mode.closer) return _sortedCharacters[0];
            else if (mode == Mode.farthest) return _sortedCharacters[_sortedCharacters.Count - 1];
        }
        return null;

    }

}
