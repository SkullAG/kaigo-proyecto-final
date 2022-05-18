using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DialogTrigger : MonoBehaviour
{
	public DialogSequence sequence;

	bool started = false;

	private void OnTriggerEnter(Collider other)
	{
		if(!started)
        {
			Debug.Log("Triggering sequence");
			DialogManager.current.StartSequence(sequence);
			started = true;
        }

		gameObject.SetActive(false);

		Debug.Log(gameObject.activeInHierarchy);
	}
}
