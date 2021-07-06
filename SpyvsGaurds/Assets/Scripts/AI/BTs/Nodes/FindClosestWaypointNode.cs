////////////////////////////////////////////////////////////
// File: <FindClosestWaypointNode.cs>
// Author: <Morgan Ellis>
// Date Created: <21/11/2020>
// Brief: <Finds the closest guard search point by looking through an array of transforms called waypoints>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestWaypointNode : Node
{
	private Waypoints[] waypoints;
	private Transform target;
	private EnemyAI ai;

	private bool startSearch;

	float shortestDistance = Mathf.Infinity;

	public FindClosestWaypointNode(Waypoints[] waypoints, Transform target, EnemyAI ai, bool startSearch)
	{
		this.waypoints = waypoints;
		this.target = target;
		this.ai = ai;
		this.startSearch = startSearch;
	}

	public override NodeState Evaluate()
	{
		Transform closestWaypoint = FindClosestWaypoint();
		ai.SetWaypoint(closestWaypoint);
		if (closestWaypoint != null)
		{
			startSearch = true;
			return NodeState.SUCCESS;
		}
		else
		{

			return NodeState.FAILURE;
		}

		//return closestWaypoint != null ? NodeState.SUCCESS : NodeState.FAILURE;
	}

	public Transform FindClosestWaypoint()
	{
		if (ai.GetWaypoint() != null)
		{
			Debug.Log("NOT NULL");
			return ai.GetWaypoint();
		}

		if (ai.GetWaypoint() == null)
		{
			Debug.Log("NULL");
		}

		Transform closestWaypoint = null;

		for (int i = 0; i < waypoints.Length; i++)
		{
			float distance = Vector3.Distance(ai.transform.position, waypoints[i].transform.position);

			if (distance < shortestDistance)
			{
				shortestDistance = distance;
				closestWaypoint = waypoints[i].transform;
				Debug.Log(closestWaypoint);
			}
		}
		return closestWaypoint;
	}
}
