using Core.Actions;
using Core.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVisuals : ActionPhase
{
	AutonomousVisualsHandler visualEffects;
	AutonomousVisualsHandler instancedObj;
	public PlayVisuals(AutonomousVisualsHandler _visualEffects)
	{
		visualEffects = _visualEffects;
	}

	public override void Start(Character actor, Character target)
	{

		base.Start(actor, target);

		if (visualEffects)
		{
			// instantiate ya sabes

			instancedObj = GameObject.Instantiate(visualEffects.gameObject).GetComponent<AutonomousVisualsHandler>();

			instancedObj.parent = this;
			instancedObj.transform.position = target.transform.position;
			instancedObj.transform.localScale = target.transform.lossyScale;
		}
		else
		{
			End();
		}

	}

	public override void Update()
	{
		if(!instancedObj)
        {
			End();
        }
	}

	public void Next()
    {
		End();
	}
}
