using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Actions;
using Core.Characters;

public class ApplyDamage : ActionPhase
{
    
    [SerializeField]
    private int _damage;

    public ApplyDamage(int damage) {

        this._damage = damage;

    }

    public override void Start() {

        Debug.Log("Applying damage: " + _damage);

        base.Start();

    }
    
    // Update phase's processing
    public override void Update(Character actor, Character[] targets) {

        //Debug.Log( "Fire!" );

        End();

    }

}
