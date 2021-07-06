////////////////////////////////////////////////////////////
// File: <SpyHideAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the spy has been spotted then it will find the closest hiding spot and hide>
////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using UnityEngine.AI;

public class SpyHideAction : GoapAction
{
	private bool hide = false;

	private float startTime = 0;
	public float hideDuration = 6; // seconds

	public SpyHideAction()
	{
		addEffect("avoidGuard", true);
		addEffect("intelClearOfEnemies", false);
	}


	public override void reset()
	{
		hide = false;
		target = null;
		startTime = 0;
	}

	public override bool isDone()
	{
		return hide;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		// find the nearest chopping block that we can chop our wood at
		HideComponent[] hiding = (HideComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(HideComponent));
		HideComponent closest = null;
		float closestDist = 0;

		foreach (HideComponent hide in hiding)
		{
			if (closest == null)
			{
				// first one, so choose it for now
				closest = hide;
				closestDist = (hide.gameObject.transform.position - agent.transform.position).magnitude;
			}
			else
			{
				// is this one closer than the last?
				float dist = (hide.gameObject.transform.position - agent.transform.position).magnitude;
				if (dist < closestDist)
				{
					// we found a closer one, use it
					closest = hide;
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
		if (startTime == 0)
			startTime = Time.time;

		if (Time.time - startTime > hideDuration)
		{
			GetComponent<Knowledge>().spyHiding = false;
			hide = true;
		}

		GetComponent<Knowledge>().spyHiding = true;
		target = null;
		return true;
	}

}

