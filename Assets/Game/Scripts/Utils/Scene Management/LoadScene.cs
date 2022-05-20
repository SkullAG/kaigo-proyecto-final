using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	[Scene]
	public string Scene;

	public void Load()
	{
		Debug.Log("loading scene:" + Scene);
		SceneManager.LoadScene(Scene, LoadSceneMode.Single);
	}
}
