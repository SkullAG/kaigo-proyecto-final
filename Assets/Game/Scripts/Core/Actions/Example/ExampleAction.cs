using UnityEngine;
using Core.Actions;
using Core.Characters;
using System.Collections.Generic;
using Core.Affinities;

/*[CreateAssetMenu(fileName = "Example Action", menuName = "Game/Actions/Example/Action")]
public class ExampleAction : GameAction
{
    
    [SerializeField]
    int _delay;

    [SerializeField]
    public AffinityList _damage;

    [SerializeField]
    float _testNumber;

    // Returns an array of the action phases that will be executed in order.
    // Configuration of this clases must be set from the inspector of this ScriptableObject.
    protected override ActionPhase[] GetPhases() {
        
        return new ActionPhase[] {

            new Delay(_delay), // Delay of X seconds
            new ApplyDamage(_damage) // Apply damage to targets

        };

    }

    // This happens once after the action is executed,
    // that is, when an actor has triggered the action.
    protected override void OnExecution() {

        // This starts the update of the action's phases.
        StartAction();

        // You can use "actor" and "targets" properties to get the
        // actor character that is executing this action and its targets.
        // Debug.Log("Action's actor is: " + actor);
        // Debug.Log("Action's first target is: " + targets[0]);
        
    }

    // This happens every frame, during the action execution.
    protected override void OnUpdate() {
        
        _testNumber += Time.deltaTime;

    }

    // This happens once after a phase starts.
    protected override void OnPhaseStart() {
        
        //Debug.Log("Phase " + currentPhase.name + " has started.");

    }
    
    // This happens once after a phase ends.
    protected override void OnPhaseEnd() {
    
        // You can control how and when the sequence of phases progress.
        // Use End() and Next() methods to customize how an
        // action plays out. 

        // Example: you can loop a phase many times or interrupt
        // its processing freely, depending on any conditions.

        //Debug.Log("Phase " + currentPhase.name + " has ended.");

        // This ends the action when the last phase has ended.
        if( OnLastPhase() ) {

            EndAction();
            return;

        }

        NextPhase(); // This jumps to the next phase.

    }

}*/
