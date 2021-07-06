////////////////////////////////////////////////////////////
// File: <SpySearchAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <Searches the area for intel and guards>
////////////////////////////////////////////////////////////

using Random = UnityEngine.Random;
using System;
using UnityEngine;

public class SpySearchAction : GoapAction
{
	private bool isSearching = false;

	public SpySearchAction()
	{
		addEffect("searchArea", true);
	}


	public override void reset()
	{
		isSearching = false;
		target = null;
	}

	public override bool isDone()
	{
		return isSearching;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{

		target = GetComponent<Spy>().GetCurrentSeachSpot();

		SpyLineOfSight SpyLineOfSight = GetComponent<SpyLineOfSight>();

		if (SpyLineOfSight.canSeeGuard || SpyLineOfSight.canSeeIntel) 
		{
			return true;
		}

		if (target != null)
		{
			return true;
		}

		return false;
	}

	public override bool perform(GameObject agent)
	{
		isSearching = true;
		GetComponent<Spy>().SetSearchSpot();
		return true;
	}

}

