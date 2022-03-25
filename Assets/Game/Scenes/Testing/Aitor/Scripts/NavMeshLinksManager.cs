using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public static class NavMeshLinksManager
{
	public static List<NavMeshLink> links = new List<NavMeshLink>();

	public enum NavMeshLinkSides
    {
		start = 1, end = 2
    }

	[RuntimeInitializeOnLoadMethod]
	static void OnRuntimeMethodLoad()
	{
		Scene scene = SceneManager.GetActiveScene();

		links.Clear();
		links.AddRange(GameObject.FindObjectsOfType<NavMeshLink>());

		//Debug.Log(links.Count);
		
		//links[0].

		//SceneManager.activeSceneChanged

		//Debug.Log("finjamos que hay ticks");
	}

	public static NavMeshLink PointIsOnLink(Vector3 point, int agentType, out NavMeshLinkSides linkSide)
    {
		Vector3 endPoint;
		Vector3 startPoint;
		Vector2 dir;

		foreach (NavMeshLink l in links)
        {
			if(l.isActiveAndEnabled && l.agentTypeID == agentType)
            {
				endPoint = l.endPoint + l.transform.position;
				startPoint = l.startPoint + l.transform.position;

				dir = new Vector2(endPoint.x - startPoint.x, endPoint.z - startPoint.z).normalized;

				if(l.bidirectional)
				{
					Vector2 pointRelativeToEnd = new Vector2(point.x - endPoint.x, point.z - endPoint.z);
					pointRelativeToEnd = CustomMath.RotateVector(pointRelativeToEnd, dir);

					if (Mathf.Abs(pointRelativeToEnd.y) <= 0.1f && Mathf.Abs(pointRelativeToEnd.x) <= l.width / 2 + 0.1f)
					{
						//Debug.Log("It's working!");
						linkSide = NavMeshLinkSides.end;
						return l;
					}
				}

				Vector2 pointRelativeToStart = new Vector2(point.x - startPoint.x, point.z - startPoint.z);
				pointRelativeToStart = CustomMath.RotateVector(pointRelativeToStart, dir);

				if(Mathf.Abs(pointRelativeToStart.y) <= 0.1f && Mathf.Abs(pointRelativeToStart.x) <= l.width/2 + 0.1f)
                {
					//Debug.Log("It's working!");
					linkSide = NavMeshLinkSides.start;
					return l;
				}
            }
        }

		linkSide = 0;
		return null;
    }

	public static bool PointIsOnCertainLink(Vector3 point, NavMeshLink link, NavMeshLinkSides linkSide = 0)
	{
		Vector3 endPoint;
		Vector3 startPoint;
		Vector2 dir;

		endPoint = link.endPoint + link.transform.position;
		startPoint = link.startPoint + link.transform.position;

		dir = new Vector2(endPoint.x - startPoint.x, endPoint.z - startPoint.z).normalized;

		Vector2 pointRelativeToEnd = new Vector2(point.x - endPoint.x, point.z - endPoint.z);
		pointRelativeToEnd = CustomMath.RotateVector(pointRelativeToEnd, dir);

		if (Mathf.Abs(pointRelativeToEnd.y) <= 0.1f && Mathf.Abs(pointRelativeToEnd.x) <= link.width / 2 + 0.1f && (linkSide == 0 || linkSide == NavMeshLinkSides.end))
		{
			//Debug.Log("It's working!");
			
			return true;
		}

		Vector2 pointRelativeToStart = new Vector2(point.x - startPoint.x, point.z - startPoint.z);
		pointRelativeToStart = CustomMath.RotateVector(pointRelativeToStart, dir);

		if (Mathf.Abs(pointRelativeToStart.y) <= 0.1f && Mathf.Abs(pointRelativeToStart.x) <= link.width / 2 + 0.1f && (linkSide == 0 || linkSide == NavMeshLinkSides.start))
		{
			//Debug.Log("It's working!");
			
			return true;
		}
		
		return false;
	}

	public static bool LineTravesingCertainLink(Vector3 start, Vector3 end, NavMeshLink link)
	{
		Vector3 endPoint;
		Vector3 startPoint;
		Vector2 dir;

		float linkLength;

		endPoint = link.endPoint + link.transform.position;
		startPoint = link.startPoint + link.transform.position;

		dir = new Vector2(endPoint.x - startPoint.x, endPoint.z - startPoint.z);

		linkLength = dir.magnitude;

		Vector2 startPointRelativeToStart = new Vector2(start.x - startPoint.x, start.z - startPoint.z);
		startPointRelativeToStart = CustomMath.RotateVector(startPointRelativeToStart, dir);

		Vector2 endPointRelativeToStart = new Vector2(end.x - startPoint.x, end.z - startPoint.z);
		endPointRelativeToStart = CustomMath.RotateVector(endPointRelativeToStart, dir);

		if(startPointRelativeToStart.y > endPointRelativeToStart.y)
        {
			Vector2 a = startPointRelativeToStart;
			startPointRelativeToStart = endPointRelativeToStart;
			endPointRelativeToStart = a;
		}

		if (((CustomMath.Aproximately(startPoint.y, start.y, 0.1f) && CustomMath.Aproximately(endPoint.y, end.y, 0.1f)) || (CustomMath.Aproximately(endPoint.y, start.y, 0.1f) && CustomMath.Aproximately(startPoint.y, end.y, 0.1f)))
			&& (startPointRelativeToStart.y <= 0.1f && endPointRelativeToStart.y >= linkLength - 0.1f)
			&& (Mathf.Abs(Mathf.Lerp(endPointRelativeToStart.x, startPointRelativeToStart.x, Mathf.Abs(startPointRelativeToStart.y) / (endPointRelativeToStart.y - startPointRelativeToStart.y))) <= link.width + 0.1f)
			&& (Mathf.Abs(Mathf.Lerp(endPointRelativeToStart.x, startPointRelativeToStart.x, (Mathf.Abs(startPointRelativeToStart.y) + linkLength) / (endPointRelativeToStart.y - startPointRelativeToStart.y))) <= link.width + 0.1f))
		{
			//Debug.Log("It's working!");

			return true;
		}

		return false;
	}

	public static Vector3 GetOppositePointOnLink(Vector3 point, NavMeshLink link, NavMeshLinkSides linkSide = 0)
	{
		Vector3 endPoint = link.endPoint + link.transform.position;
		Vector3 startPoint = link.startPoint + link.transform.position;
		Vector2 dir = new Vector2(endPoint.x - startPoint.x, endPoint.z - startPoint.z).normalized;

		Vector2 pointRelativeToEnd = new Vector2(point.x - endPoint.x, point.z - endPoint.z);
		pointRelativeToEnd = CustomMath.RotateVector(pointRelativeToEnd, dir);

		Vector2 pointRelativeToStart = new Vector2(point.x - startPoint.x, point.z - startPoint.z);
		pointRelativeToStart = CustomMath.RotateVector(pointRelativeToStart, dir);

		if((linkSide == 0 && CustomMath.CloseTo0(pointRelativeToStart.y, pointRelativeToEnd.y) == pointRelativeToStart.y) || linkSide == NavMeshLinkSides.start)
        {
			return (point - startPoint) + endPoint;
		}
		else
        {
			return (point - endPoint) + startPoint;
		}
	}
}
