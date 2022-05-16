using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tick
{
	static float navTicksPerSecond = 2;

    [RuntimeInitializeOnLoadMethod]
	static void OnRuntimeMethodLoad()
	{
		//Debug.Log("finjamos que hay ticks");
		//NavigationTick();
	}

    private static IEnumerator NavigationTick()
    {
		while(true)
        {
			//yield return UnityEngine.WaitForSeconds(1 / navTicksPerSecond);
        }
    }
}
