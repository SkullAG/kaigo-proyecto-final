using UnityEngine;
using Core.Actions;
using Core.Characters;
using System.Collections.Generic;
using Core.Affinities;
using Core.States;

//[CreateAssetMenu(fileName = "Item Action", menuName = "Game/Actions/Item Action")]
[System.Serializable]
public class ItemAction : GameAction
{

    // Aquí debe haber variables públicas para configurar el comportamiento de las
    // fases

    public int valueRecovered; // El valor recuperado de vida o puntos de acción
    public float distanceToCast; // La distancia máxima a la que el personaje puede usar el objeto
    public bool blockMovement = true;
    public State[] states; // Los estados alterados que aplica este objeto (puede ser ninguno)

    public override GameAction Copy() {

        return (ItemAction)this.MemberwiseClone();

    }

    // Esta función tiene que devolver las fases que tiene el tipo de acción
    protected override ActionPhase[] GetPhases() {
        
        // Estas son las fases de la acción
        return new ActionPhase[] {

            new MoveToTarget(distanceToCast), // Se mueve hacia el objetivo 

            new PlayAnimation(id, blockMovement), // Reproduce una animación con el mismo ID que la acción creada

            new ApplyRecovery(valueRecovered), // <---- Aquí se produce la recuperación de vida o puntos de acción

            new ApplyState(states) // Aplica un estado alterado sobre el objetivo

        };

    }

    // This happens once after the action is executed,
    // that is, when an actor has triggered the action.
    protected override void OnExecution() {


        // This starts the update of the action's phases.
        StartAction();

        BattleLog.current.WriteLine(string.Format(BattleLogFormats.ITEM_USE, actor.name, displayName));

        // You can use "actor" and "targets" properties to get the
        // actor character that is executing this action and its targets.
        // Debug.Log("Action's actor is: " + actor);
        // Debug.Log("Action's first target is: " + targets[0]);
        
    }

    // This happens every frame, during the action execution.
    protected override void OnUpdate() {}

    // This happens once after a phase starts.
    protected override void OnPhaseStart() {}
    
    protected override void OnPhaseEnd() {
    
        if( OnLastPhase() ) {

            EndAction(); 

            return;

        }

        NextPhase();

    }

    // Devuelve true si la acción es utilizable por el personaje
    public override bool IsUsableBy(Character character)
    {
        
        return true;

    }

}
