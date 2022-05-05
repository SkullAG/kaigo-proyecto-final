using UnityEngine;
using Core.Actions;
using Core.States;
using Core.Affinities;
using Core.Characters;
using NaughtyAttributes;
using System;

//[CreateAssetMenu(fileName = "Battle Action", menuName = "Game/Actions/Battle Action")]
[System.Serializable]
public class BattleAction : GameAction
{

    public string animationState;

    public AffinityList damage;

    public int costOverHP = 0;
    public int costOverAP = 0;

    public float distanceToCast;
    public bool blockMovement = true;
	public StateAndProbability[] states;

    [HideInInspector] public bool hasHPCost => costOverHP > 0;
    [HideInInspector] public bool hasAPCost => costOverAP > 0;

    private float timer = 0;
    private int counter = 0;

	//public bool applyState = true;
	//[ShowIf("applyState")]

	private bool casting = false;

	public override GameAction Copy() {

		return (BattleAction)this.MemberwiseClone();
		
	}

	protected override ActionPhase[] GetPhases() {

		return new ActionPhase[] {

			new MoveToTarget(distanceToCast), // Move to target, stop at X distance
			new PlayAnimation(animationState, blockMovement), // Start animation with the action id
			new ApplyDamage(damage),
			new ApplyState(states)

		};

	}

	protected override void OnExecution() {

		StartAction();

		BattleLog.current.WriteLine(string.Format(BattleLogFormats.SKILL_CHARGE, actor.name, displayName));

		ApplyCost();
		
	}

    private void ApplyCost() {

        actor.stats.healthPoints.value -= costOverHP;
        actor.stats.actionPoints.value -= costOverAP;

    }

	protected override void OnUpdate() {}

	protected override void OnPhaseStart() {}
	
	protected override void OnPhaseEnd() {
	
		if( OnLastPhase() ) {

			EndAction();
			BattleLog.current.WriteLine(string.Format(BattleLogFormats.SKILL_USE, actor.name, displayName));

			return;

		}

		NextPhase();

	}

    public override bool IsUsableBy(Character character) {
        
        int _hp = character.stats.healthPoints.value - costOverHP;
        int _ap = character.stats.actionPoints.value - costOverAP;

        return _hp >= 0 && _ap >= 0;

    }

}
