using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;

[CreateAssetMenu(fileName = "Apply Damage", menuName = "Game/Actions/Phases/Apply Damage")]
public class ApplyDamage : ActionPhase
{
    
    [SerializeField]
    private int _damage;
    
    // Update phase's processing
    public override void UpdateLogic(Character actor, Character[] targets) {

        Debug.Log( "Fire!" );

        End();

    }

}
