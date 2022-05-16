using Core.Characters;
using Core.States;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathEffect", menuName = "Game/States/Effects/Death")]
public class Death : Effect
{

    public override void OnEffectActivated(Character actor) {
        
        Debug.Log("Applying death!");

        actor.navBody.isParalized = true;
        
        // Poner animacion de muerte aqui

    }

}
