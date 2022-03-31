using UnityEngine;
using Core.Actions;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Spell", menuName = "Game/Actions/Spell")]
public class SpellAction : GameAction
{

    public string castingAnimation;
    public float castingEndTime;

    [Space(15)]

    public string spellAnimation;
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

            new PlayAnimation(castingAnimation, castingEndTime),
            new PlayAnimation(spellAnimation, spellEndtime),
            new ApplyDamage(damage),

        };

    }

    protected override void OnExecution() {

        StartAction();

        Debug.Log("Executing spell!");

        ApplyCost(); // Cost is applied at the start
        
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

    private void ApplyCost() {

        actor.stats.actionPoints.value -= cost;

    }

}
