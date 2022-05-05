using UnityEngine;
using Core.Actions;
using Core.States;
using Core.Affinities;
using NaughtyAttributes;
using System;

//[CreateAssetMenu(fileName = "Battle Action", menuName = "Game/Actions/Battle Action")]
[System.Serializable]
public class BattleAction : GameAction
{

	public AffinityList damage;
	public int cost = 0;
	public float distanceToCast;


	//public bool applyState = true;
	//[ShowIf("applyState")]
	public StateAndProbability[] states;

	public bool blockMovement = true;

	private float timer = 0;
	private int counter = 0;

	private bool casting = false;

	public override GameAction Copy() {

		return (BattleAction)this.MemberwiseClone();
		
	}

	protected override ActionPhase[] GetPhases() {

		return new ActionPhase[] {

			new MoveToTarget(distanceToCast), // Move to target, stop at X distance
			new PlayAnimation(id, blockMovement), // Start animation with the action id
			new ApplyDamage(damage),
			new ApplyState(states)

		};

	}

	protected override void OnExecution() {

		StartAction();

		BattleLog.current.WriteLine(string.Format(BattleLogFormats.SKILL_CHARGE, actor.name, displayName));

		actor.stats.actionPoints.value -= cost; // Cost is applied at the start
		
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

}
