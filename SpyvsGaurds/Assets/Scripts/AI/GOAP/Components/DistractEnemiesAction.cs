
using System;
using UnityEngine;
using UnityEngine.AI;

public class DistractEnemiesAction : GoapAction
{
	private bool distracted = false;

	Knowledge knowledge;

	public DistractEnemiesAction()
	{
		addEffect("intelClearOfEnemies", true);
	}

	private void Start()
	{
		knowledge = GetComponent<Knowledge>();
	}

	public override void reset()
	{
		distracted = false;
		target = null;
	}

	public override bool isDone()
	{
		return distracted;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		// find the nearest chopping block that we can chop our wood at
		DistractionComponent[] distraction = (DistractionComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(DistractionComponent));
		DistractionComponent closest = null;
		float closestDist = 0;

		foreach (DistractionComponent distract in distraction)
		{
			if (closest == null)
			{
				// first one, so choose it for now
				closest = distract;
				closestDist = (distract.gameObject.transform.position - agent.transform.position).magnitude;
			}
			else
			{
				// is this one closer than the last?
				float dist = (distract.gameObject.transform.position - agent.transform.position).magnitude;
				if (dist < closestDist)
				{
					// we found a closer one, use it
					closest = distract;
					closestDist = dist;
				}
			}
		}
		if (closest == null)
			return false;

		target = closest.gameObject;


		return closest != null;
	}

	public override bool perform(GameObject agent)
	{
		distracted = true;
		knowledge.spyDistracting = true;
		knowledge.currrentDistraction = target;
		return true;
	}

}

