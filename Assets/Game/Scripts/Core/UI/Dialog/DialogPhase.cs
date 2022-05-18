using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSPhase", menuName = "Game/Dialog/Phase")]
public class DialogPhase : ScriptableObject
{
	[SerializeReference]
	public List<DialogAction> actions = new List<DialogAction>();

	public DialogAction.dialogActionType type;

	public void Play(DialogManager dialogManager)
	{
		Debug.Log("playing phase " + this.name);
		foreach (DialogAction a in actions)
		{
			a.PlayAction(dialogManager);
		}
	}

	public void Abort(DialogManager dialogManager)
	{
		foreach (DialogAction a in actions)
		{
			a.AbortAction(dialogManager);
		}
	}

	[Button]
	public void createAction()
	{
		switch (type)
		{
			case DialogAction.dialogActionType.EmptyAction:
				actions.Add(new DialogAction());
				break;
			case DialogAction.dialogActionType.TransformTranslate:
				actions.Add(new TransformScale());
				break;
			case DialogAction.dialogActionType.TransformRotate:
				actions.Add(new TransformRotate());
				break;
			case DialogAction.dialogActionType.TransformScale:
				actions.Add(new TransformScale());
				break;
			case DialogAction.dialogActionType.WriteText:
				actions.Add(new WriteText());
				break;
			case DialogAction.dialogActionType.PlaySound:
				actions.Add(new PlaySound());
				break;
			case DialogAction.dialogActionType.PlayAnimation:
				actions.Add(new PlayAnim());
				break;
			case DialogAction.dialogActionType.TriggerEvent:
				actions.Add(new TriggerEvent());
				break;
		}
	}
}

[System.Serializable]
public class DialogAction
{
	public enum dialogActionType
	{
		EmptyAction, TransformTranslate, TransformRotate, TransformScale, WriteText, PlaySound, PlayAnimation, TriggerEvent
	}

	public virtual void PlayAction(DialogManager dialogManager)
	{

	}

	public virtual void AbortAction(DialogManager dialogManager)
	{

	}
}

public class TransformTranslate : DialogAction
{
	public bool lerpOverTime = false;

	[ShowIf("lerpOverTime")]
	public float duration;

	public int objIndex;
	public Vector3 endPosition;

	public override void PlayAction(DialogManager dialogManager)
	{
		if (lerpOverTime)
		{
			dialogManager.StartCoroutine(MoveOverTime(dialogManager.transformList[objIndex], endPosition, duration));
		}
		else
		{
			dialogManager.transformList[objIndex].position = endPosition;

		}
	}

	public override void AbortAction(DialogManager dialogManager)
	{
		dialogManager.StopCoroutine(MoveOverTime(dialogManager.transformList[objIndex], endPosition, duration));
	}

	IEnumerator MoveOverTime(Transform obj, Vector3 pos, float time)
	{
		Vector3 initPos = obj.position;

		for (float timer = 0; timer <= time; timer++)
		{
			obj.position = Vector3.Lerp(initPos, pos, timer / time);

			yield return new WaitForEndOfFrame();
		}
	}
}

public class TransformRotate : DialogAction
{
	public bool lerpOverTime = false;

	[ShowIf("lerpOverTime")]
	public float duration;

	public int objIndex;

	public Quaternion rotation;

	public override void PlayAction(DialogManager dialogManager)
	{
		if (lerpOverTime)
		{
			dialogManager.StartCoroutine(RotateOverTime(dialogManager.transformList[objIndex], rotation, duration));
		}
		else
		{
			dialogManager.transformList[objIndex].rotation = rotation;

		}
	}

	public override void AbortAction(DialogManager dialogManager)
	{
		dialogManager.StopCoroutine(RotateOverTime(dialogManager.transformList[objIndex], rotation, duration));
	}

	IEnumerator RotateOverTime(Transform obj, Quaternion rot, float time)
	{
		Quaternion initRot = obj.rotation;

		for (float timer = 0; timer <= time; timer++)
		{
			obj.rotation = Quaternion.Lerp(initRot, rot, timer / time);

			yield return new WaitForEndOfFrame();
		}
	}
}

public class TransformScale : DialogAction
{
	public bool lerpOverTime = false;

	[ShowIf("lerpOverTime")]
	public float duration;

	public int objIndex;

	public Vector3 scale;

	public override void PlayAction(DialogManager dialogManager)
	{
		if (lerpOverTime)
		{
			dialogManager.StartCoroutine(ScaleOverTime(dialogManager.transformList[objIndex], scale, duration));
		}
		else
		{
			dialogManager.transformList[objIndex].position = scale;

		}
	}

	public override void AbortAction(DialogManager dialogManager)
	{
		dialogManager.StopCoroutine(ScaleOverTime(dialogManager.transformList[objIndex], scale, duration));
	}

	IEnumerator ScaleOverTime(Transform obj, Vector3 loc, float time)
	{
		Vector3 initLoc = obj.localScale;

		for (float timer = 0; timer <= time; timer++)
		{
			obj.localScale = Vector3.Lerp(initLoc, loc, timer / time);

			yield return new WaitForEndOfFrame();
		}
	}
}

public class WriteText : DialogAction
{
	public string Speaker = "";
	public string Text = "";

	public override void PlayAction(DialogManager dialogManager)
	{

		dialogManager._ds.Write(Text, Speaker);
	}

	public override void AbortAction(DialogManager dialogManager)
	{
		dialogManager._ds.Clear();
	}
}

public class PlaySound : DialogAction
{
	public AudioClipAsset audioClipAsset;

	public bool is3D = false;

	public Vector3 position;

	public override void PlayAction(DialogManager dialogManager)
	{
		if (is3D)
		{
			AudioManager.current.Play3D(audioClipAsset, position);
		}
		else
		{
			AudioManager.current.Play(audioClipAsset);
		}
	}

	public override void AbortAction(DialogManager dialogManager)
	{

	}
}

public class PlayAnim : DialogAction
{
	public int animatorIndex;
	public string animationState;

	public override void PlayAction(DialogManager dialogManager)
	{
		dialogManager.animatorList[animatorIndex].Play(animationState);
	}

	public override void AbortAction(DialogManager dialogManager)
	{
	}
}

public class TriggerEvent : DialogAction
{
	public int eventIndex;

	public override void PlayAction(DialogManager dialogManager)
	{
		dialogManager.eventList[eventIndex].Invoke();
	}

	public override void AbortAction(DialogManager dialogManager)
	{

	}
}