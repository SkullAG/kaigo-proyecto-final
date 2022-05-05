using Core.Characters;
using Core.States;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathEffect", menuName = "Game/States/Effects/Death")]
public class Death : Effect
{

    public override void Apply(Character actor, float power = Mathf.Infinity /*Esta muerto pero muy fuerte!*/) { //pq muerte es un estado? bueno, se puede estar muerto un tiempo?! me interesa la idea
        
        Debug.Log("Applying death!");

        actor.gameObject.SetActive(false);

    }

}
