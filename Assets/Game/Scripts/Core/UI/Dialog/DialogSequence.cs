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

	public void PlayPhase(int index, DialogManager dialogManager)
	{
		phases[index].Play(dialogManager);
	}

	public void AbortPhase(int index, DialogManager dialogManager)
	{
		phases[index].Abort(dialogManager);
	}
}
