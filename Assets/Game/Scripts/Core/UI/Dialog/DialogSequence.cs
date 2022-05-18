using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogSequence", menuName = "Game/Dialog/Sequence")]
public class DialogSequence : ScriptableObject
{
	[SerializeField]
	public List<DialogPhase> phases = new List<DialogPhase>();

	[SerializeField]
	public DialogPhase EndState;

	public void PlayPhase(int index, DialogManager dialogManager)
	{
		phases[index].Play(dialogManager);
	}

	public void AbortPhase(int index, DialogManager dialogManager)
	{
		phases[index].Abort(dialogManager);
	}

	public void PlayEnd(DialogManager dialogManager)
	{
		AbortPhase(phases.Count - 1, dialogManager);
		EndState.Play(dialogManager);
		EndState.Abort(dialogManager);
	}
}
