using Core.Characters;
using Core.States;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathEffect", menuName = "Game/States/Effects/Death")]
public class Death : Effect
{

    public override void OnEffectActivated(Character actor) {
        
        Debug.Log("I should be death by now!");

        actor.GetComponent<NavBodyAnimatorManager>().animator.SetBool("Dead", true);

        actor.navBody.isParalized = true;
        actor.gambits.SetEnabled(false);
        actor.queue.update = false;
        
        // Poner animacion de muerte aqui

    }

    public override void OnEffectExpired(Character actor)
    {

        Debug.Log("I should be undeath by now!");

        actor.GetComponent<NavBodyAnimatorManager>().animator.SetBool("Dead", false);

        actor.navBody.isParalized = false;
        actor.gambits.SetEnabled(true);
        actor.queue.update = true;

        // Poner animacion de muerte aqui

    }

}
