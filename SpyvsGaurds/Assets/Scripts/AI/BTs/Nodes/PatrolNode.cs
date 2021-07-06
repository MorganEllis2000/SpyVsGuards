////////////////////////////////////////////////////////////
// File: <PatrolNode.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This chooses a patrol path for the guard and allows it to patrol along this path>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNode : Node
{
	private List<Transform> moveSpots;
	private NavMeshAgent agent;
	private EnemyAI ai;

	public PatrolNode(List<Transform> moveSpots, NavMeshAgent agent, EnemyAI ai)
	{
		this.moveSpots = moveSpots;
		this.agent = agent;
		this.ai = ai;
	}

	private int destPoint = 0;
	private float waitTime;
	private float startWaitTime = 2f;

	public override NodeState Evaluate()
	{
		ai.SetColor(Color.black);
		waitTime = startWaitTime;
		if (!agent.pathPending && agent.remainingDistance < 0.1f)
		{
			GoToNextPoint();
			return NodeState.SUCCESS;
		}
		else
		{
			return NodeState.RUNNING;
		}
	}

	void GoToNextPoint()
	{
		int sizeofList = moveSpots.Count;
		//Debug.Log("Size of List: " + sizeofList);
		if (sizeofList == 0)
		{
			return;
		}

		agent.autoBraking = true;

		agent.destination = moveSpots[destPoint].position;

		destPoint = (destPoint + 1) % sizeofList;
	}
}
