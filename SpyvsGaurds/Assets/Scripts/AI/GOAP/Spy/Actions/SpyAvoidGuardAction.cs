////////////////////////////////////////////////////////////
// File: <SpyAvoidGuardAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the guard is in the spys path then the spy needs to avoid it>
////////////////////////////////////////////////////////////

using Random = UnityEngine.Random;
using System;
using UnityEngine;
using UnityEngine.AI;

public class SpyAvoidGuardAction : GoapAction
{
	private bool avoidingGuard = false;

	public SpyAvoidGuardAction()
	{
		addEffect("avoidGuard", true); // we need a tool to do this
	}


	public override void reset()
	{
		avoidingGuard = false;
		target = null;
	}

	public override bool isDone()
	{
		return avoidingGuard;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		SpyLineOfSight spyLineOfSight = GetComponent<SpyLineOfSight>();



		if (target != null)
		{
			return true;
		}

		return false;
	}

	public override bool perform(GameObject agent)
	{
		avoidingGuard = true;
		return true;
	}

}

