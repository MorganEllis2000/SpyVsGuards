////////////////////////////////////////////////////////////
// File: <SpyEscapeAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the spy has the intel then the spy can escape>
////////////////////////////////////////////////////////////
///
using System;
using UnityEngine;
using UnityEngine.AI;

public class SpyEscapeAction : GoapAction
{
	private bool hasEscaped = false;

	private float startTime = 0;
	public float workDuration = 2; // seconds

	public SpyEscapeAction()
	{
		addPrecondition("getIntel", true); // we need a tool to do this
		addEffect("Escape", true);
	}


	public override void reset()
	{
		hasEscaped = false;
		target = null;
		startTime = 0;
	}

	public override bool isDone()
	{
		return hasEscaped;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		// find the nearest chopping block that we can chop our wood at
		EscapeComponent[] escapes = (EscapeComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(EscapeComponent));
		EscapeComponent closest = null;
		float closestDist = 0;

		foreach (EscapeComponent escape in escapes)
		{
			if (closest == null)
			{
				// first one, so choose it for now
				closest = escape;
				closestDist = (escape.gameObject.transform.position - agent.transform.position).magnitude;
			}
			else
			{
				// is this one closer than the last?
				float dist = (escape.gameObject.transform.position - agent.transform.position).magnitude;
				if (dist < closestDist)
				{
					// we found a closer one, use it
					closest = escape;
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

		if (Time.time - startTime > workDuration)
		{
			hasEscaped = true;
		}
		return true;
	}

}

