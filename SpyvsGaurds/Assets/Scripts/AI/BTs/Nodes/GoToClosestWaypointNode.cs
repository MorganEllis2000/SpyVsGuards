////////////////////////////////////////////////////////////
// File: <GoToClosestWaypointNode.cs>
// Author: <Morgan Ellis>
// Date Created: <21/11/2020>
// Brief: <Using the search point that was chosen in the FindClosestWaypointNode the guard then goes towards this waypoint>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToClosestWaypointNode : Node
{
	private NavMeshAgent agent;
	private EnemyAI ai;
	private float speed = 3f;
	private bool arrivedAtWaypoint;

	BlackBoard blackboard;
	private bool startSearch;

	public GoToClosestWaypointNode(NavMeshAgent agent, EnemyAI ai, bool startSearch)
	{
		this.agent = agent;
		this.ai = ai;
		this.startSearch = startSearch;
		
	}

	public override NodeState Evaluate()
	{
		Transform waypoint = ai.GetWaypoint();
		if (waypoint == null)
		{
			Debug.Log("<color=Red>Action Failed: GoToClosestWaypointNode -> NULL</color>");
			return NodeState.FAILURE;
		}

		ai.SetColor(Color.blue);

		agent.destination = waypoint.transform.position;
		agent.autoBraking = true;

		blackboard = GameObject.Find("Blackboard").GetComponent<BlackBoard>();

		if (!agent.pathPending && agent.remainingDistance > 0.1f)
		{
			return NodeState.RUNNING;
		}
		else if (agent.remainingDistance < 0.1f)
		{
			blackboard.spyBeenSpotted = false;
			agent.ResetPath();
			ai.ResetWaypoint();
			return NodeState.SUCCESS;
		}
		else
		{
			return NodeState.FAILURE;
		}
	}
}
