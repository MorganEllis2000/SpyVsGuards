////////////////////////////////////////////////////////////
// File: <DistractionNode.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the spy has set off a distraction the guard will go to the distraction>
////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistractionNode : Node
{
	private GameObject currentDistraction;
	Knowledge knowledge;
	NavMeshAgent agent;
	EnemyAI ai;

	private float startTime = 0;
	private float distractTime = 10f;

	public DistractionNode(NavMeshAgent agent, EnemyAI ai)
	{
		this.agent = agent;
		this.ai = ai;
	}

	public override NodeState Evaluate()
	{
		knowledge = GameObject.FindGameObjectWithTag("Spy").GetComponent<Knowledge>();
		currentDistraction = knowledge.currrentDistraction;

		if (currentDistraction == null)
		{
			return NodeState.FAILURE;
		}

		if (knowledge.spyDistracting == true)
		{
			agent.destination = currentDistraction.transform.position;
			if (startTime == 0)
			{
				startTime = Time.time;
			}

			if (Time.time - startTime > distractTime && agent.remainingDistance < 1f)
			{
				agent.isStopped = true;
			}

			if (Time.time - startTime < distractTime)
			{
				knowledge.spyDistracting = false;
				knowledge.currrentDistraction = null;
				return NodeState.SUCCESS;
			}
			return NodeState.RUNNING;
		}
		else
		{
			return NodeState.FAILURE;
		}
	}
}
