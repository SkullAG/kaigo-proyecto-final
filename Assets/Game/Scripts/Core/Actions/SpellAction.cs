using UnityEngine;
using Core.Actions;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Spell", menuName = "Game/Actions/Spell")]
public class SpellAction : GameAction
{

    public float castingEndTime;
    public float distanceToCast;

    [Space(15)]

    //public string spellAnimation;
    public float spellEndtime;

    [Space(15)]

    public int cost = 0;
    public int castingDelay = 0;
    public int damage = 0;

    private float timer = 0;
    private int counter = 0;

    private bool casting = false;

    protected override ActionPhase[] GetPhases() {

        return new ActionPhase[] {

            new MoveToTarget(distanceToCast), // Move to target, stop at X distance
            new PlayAnimation(id, 0), // Start spell animation using action ID
            new ApplyDamage(damage),

        };

    }

    protected override void OnExecution() {

        StartAction();

        Debug.Log("Executing spell!");

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
