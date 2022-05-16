using System.Collections.Generic;
using UnityEngine;
using Core.Characters;
using NaughtyAttributes;
using System.Linq;

namespace Core.AI
{

    public abstract class TargetFilter : ScriptableObject
    {
        
        public string displayName = "NO_NAME";
        public string id;
        [TextArea] public string description = "NO_DESC";

        public Scope filterScope;
        public bool excludeSelf = true;

        //[System.Flags] public enum Scope { none = 0, allies = 1 << 0, enemies = 1 << 1}
        public enum Scope { playerGroup = 1, fieldMobs = 2 } // No funciona aliados + enemigos de momento

        public abstract Character GetTarget(Character actor);
        public abstract void DrawGizmos(Character actor);

        public HashSet<Character> GetValidCharacters(Character actor, Scope scope) {

            PartyController _party = PartyController.current;
            BattleController _battle = BattleController.current;

            if(filterScope == Scope.playerGroup) { // Don't use target filters with ally scope for enemies

                HashSet<Character> _list = new HashSet<Character>(_party.members);

                if(excludeSelf) _list.Remove(actor);

                // Return list of allies
                return _list;

            } else if (filterScope == Scope.fieldMobs) {

                // if there are enemies currently engaged
                if(_battle.enemies.Count > 0) { 

                    HashSet<Character> _list = new HashSet<Character>(_battle.enemies);

                    if(excludeSelf) _list.Remove(actor);
                    
                    // Return list of enemies
                    return _list;

                }   

            } /*else if (filterScope == (Scope.enemies & Scope.allies)) { 

                Debug.Log("All");

                var _partyMembers = new HashSet<Character>(_party.members);
                var _enemies = new HashSet<Character>(_battle.enemies);

                // Return all characters, both allies and enemies
                return (HashSet<Character>)_partyMembers.Union(_enemies);;

            }*/

            return null;

        }

    }
    
}

