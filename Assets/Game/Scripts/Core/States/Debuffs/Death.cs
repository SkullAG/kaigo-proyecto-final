using Core.Characters;
using Core.States;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathEffect", menuName = "Game/States/Effects/Death")]
public class Death : Effect
{

    public override void Apply(Character actor) {
        
        Debug.Log("Applying death!");

        actor.gameObject.SetActive(false);

    }

}
