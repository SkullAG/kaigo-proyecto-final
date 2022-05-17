using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

using Core.Characters;

public class BattleController : Singleton<BattleController>
{
    
    [Tag] public string enemyTag = "Enemy";
    [Tag] public string allyTag = "Ally";

    private PartyController _partyController;

    private HashSet<Character> _party = new HashSet<Character>();
    private HashSet<Character> _enemies = new HashSet<Character>();

    public HashSet<Character> enemies => _enemies;

    [ShowNativeProperty, SerializeField]
    public int enemyCount => _enemies.Count;

    public bool enemiesAround => _enemies.Count > 0;

    private void Update() {

        PartyController _controller = PartyController.current;

        // If there are any party member in action
        if(_controller.IsPartyInAction()) {

            _party = new HashSet<Character>(_controller.members);

            // Update enemy list
            UpdateEnemyList();

        }

    }

    private void UpdateEnemyList() {

        _enemies?.Clear();

        // For each party member
        foreach (Character partyMember in _party)  {
            
            // If member is being targetted or an action is being casted to an enemy
            if(partyMember.isBeingTargetted) {

                foreach (Character other in partyMember.targettedBy) {

                    // By an enemy
                    if(other.CompareTag(enemyTag)) {

                        // Track enemies targetting any party member
                        _enemies.Add(other);

                        //Debug.Log("Found enemy: " + other.name);

                    }

                }

            // If member is targetting an enemy 
            } else if (partyMember.queue.isPerformingAction && partyMember.queue.currentTarget != null) {

                if(partyMember.queue.currentTarget.CompareTag(enemyTag)) {

                    Character _target = partyMember.queue.currentTarget;

                    _enemies.Add(_target);

                    //Debug.Log("Found enemy: " + _target.name);

                }

            }

        }

        //Debug.Log("Update enemy list: " + _enemies);

    }

}
