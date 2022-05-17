using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Character Count Condition", menuName = "Game/AI/Conditions/Character Count Condition")]
public class CharacterCountCondition : BehaviourCondition
{

    public enum Mode { greaterThan, lessThan }
    public enum Group { fieldMobs, partyMembers }

    public int enemyCount = 0;
    public Mode mode;
    public Group group;

    public override bool Evaluate(Character actor, Character targets) {
        
        int _count = 0; 
        
        if(group == Group.fieldMobs) {

            _count = BattleController.current.enemyCount;

        } else if (group == Group.partyMembers) {

            _count = PartyController.current.members.Length;

        }

        if(mode == Mode.greaterThan) {

            return _count >= enemyCount;

        } else {

            return _count <= enemyCount;

        }

    }

}
