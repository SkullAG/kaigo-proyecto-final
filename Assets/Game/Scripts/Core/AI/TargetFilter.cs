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

        //[System.Flags] public enum Scope { none = 0, allies = 1 << 0, enemies = 1 << 1}
        public enum Scope { allies = 1, enemies = 2 } // No funciona aliados + enemigos de momento

        public abstract Character GetTarget(Character actor);
        public abstract void DrawGizmos(Character actor);

        public HashSet<Character> GetValidCharacters(Scope scope) {

            PartyController _party = PartyController.current;
            BattleController _battle = BattleController.current;

            if(filterScope == Scope.allies) {

                // Return list of allies
                return new HashSet<Character>(_party.members);

            } else if (filterScope == Scope.enemies) {

                // if there are enemies currently engaged
                if(_battle.enemies.Count > 0) { 
                    
                    // Return list of enemies
                    return new HashSet<Character>(_battle.enemies);

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

