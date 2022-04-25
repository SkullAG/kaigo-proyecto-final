using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraManager))]
[ExecuteInEditMode]
public class DistanceCutManager : MonoBehaviour
{
	[Range(0,1)]
	public float nearCut = 0;

	CameraManager cam;
	void Start()
	{
		cam = GetComponent<CameraManager>();
	}

	// Update is called once per frame
	void Update()
	{
		Shader.SetGlobalFloat("_DistanceCut", cam.objective ? (cam.objective.position - cam.transform.position).magnitude : 0);
		Shader.SetGlobalFloat("_AlphaCut", nearCut);
	}
}
