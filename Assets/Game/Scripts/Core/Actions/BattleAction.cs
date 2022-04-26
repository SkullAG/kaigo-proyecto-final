using UnityEngine;
using Core.Actions;
using Core.States;
using Core.Affinities;

[CreateAssetMenu(fileName = "Battle Action", menuName = "Game/Actions/Battle Action")]
public class BattleAction : GameAction
{

    public AffinityList damage;
    public int cost = 0;
    public float distanceToCast;
    public bool instantCast;
    public State state;

    private float timer = 0;
    private int counter = 0;

    private bool casting = false;

    protected override ActionPhase[] GetPhases() {

        return new ActionPhase[] {

            new MoveToTarget(distanceToCast), // Move to target, stop at X distance
            new PlayAnimation(id, 0, instantCast), // Start spell animation using action ID
            new ApplyDamage(damage),
            new ApplyState(state)

        };

    }

    protected override void OnExecution() {

        StartAction();

        Debug.Log("Executing spell attack!");

        actor.stats.actionPoints.value -= cost; // Cost is applied at the start
        
    }

    protected override void OnUpdate() {
        
        

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

}
