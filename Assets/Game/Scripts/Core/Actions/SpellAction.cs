using UnityEngine;
using Core.Actions;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Spell", menuName = "Game/Actions/Spell")]
public class SpellAction : GameAction
{

    public int cost = 0;
    public int castingDelay = 0;

    private float timer = 0;
    private int counter = 0;

    private bool casting = false;

    protected override ActionPhase[] GetPhases() {

        return new ActionPhase[] {

            

        };

    }

    protected override void OnExecution() {

        StartAction();

        Debug.Log("Executing spell!");

        ApplyCost(); // Cost is applied at the start
        
    }

    protected override void OnUpdate() {
        
        timer += Time.deltaTime;

        if(timer >= 1) {

            timer = 0;
            counter ++;

            if(counter >= castingDelay) {
                casting = false;
            }

        }

    }

    protected override void OnPhaseStart() {
        
        

    }
    
    protected override void OnPhaseEnd() {
    
        if( OnLastPhase() ) {

            EndAction();
            return;

        }

        NextPhase();

    }

    private void ApplyCost() {

        actor.stats.actionPoints.value -= cost;

    }

}
