using UnityEngine;
using Core.Characters;
using NaughtyAttributes;
using Core.Actions.Example;

public class BattleTester : MonoBehaviour
{
    
    private Character attacker;
    private Character defender;

    private SkillAction action;

    [Button] 
    private void UseAction() {

        action.Execute( attacker, new Character[] {defender} );

    }

}
