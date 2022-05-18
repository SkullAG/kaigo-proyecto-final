using Core.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
	public DialogSystem _ds;

	public List<Transform> transformList = new List<Transform>();

	public List<Animator> animatorList = new List<Animator>();

	public List<UnityEvent> eventList = new List<UnityEvent>();

	//public List<>
	public bool isOn = false;

	public static DialogManager current;

	public DialogSequence currentSequence;
	public int currentPhaseIndex;

	public InputActionReference action;

	private void Awake()
	{
		DialogManager.current = this;
	}

	public void LoadInformer(DialogManagerInformer informer)
	{
		if(informer._ds)_ds = informer._ds;

		foreach(Transform t in informer.transformList)
		{
			transformList.Add(t);
		}

		foreach (Animator a in informer.animatorList)
		{
			animatorList.Add(a);
		}

		foreach (UnityEvent e in informer.eventList)
		{
			eventList.Add(e);
		}
	}

	public void StartSequence(DialogSequence sequence)
	{
		Debug.Log("Starting sequence");

		if(currentSequence)
		{
			currentSequence.AbortPhase(currentPhaseIndex, this);
		}

		currentSequence = sequence;

		isOn = true;

		// Desactivar cosas

		SetActiveState(false);

		currentPhaseIndex = 0;

		currentSequence.PlayPhase(currentPhaseIndex, this);
	}

	public void Update()
	{
		if (action.action.triggered && isOn && currentSequence)
		{
			NextPhase();
		}
	}

	public void NextPhase()
	{
		currentSequence.AbortPhase(currentPhaseIndex, this);

		currentPhaseIndex++;

		if(currentSequence.phases.Count <= currentPhaseIndex)
		{
			EndSequence();
		}
		else
		{
			currentSequence.PlayPhase(currentPhaseIndex, this);
		}
	}

	public void EndSequence()
	{
		Debug.Log("Exiting dialog");
		if (currentSequence)
		{
			currentSequence.PlayEnd(this);
		}

		// Activar cosas
		SetActiveState(true);

		currentSequence = null;

		isOn = false;

		currentPhaseIndex = 0;
	}

	void SetActiveState(bool state)
    {
		Debug.Log("setting to " + state);
		CommandController.current.gameObject.SetActive(state);
		BattleLog.current.gameObject.SetActive(state);
		PartyUIDrawer.current.gameObject.SetActive(state);
		UICommandHelp.current.gameObject.SetActive(state);
		foreach (NavBodySistem a in PartyManager.current.getPartyMembers())
		{
			a.isParalized = !state;
		}
		CameraManager.current.enabled = state;
		PartyManager.current.enabled = state;
	}
}
