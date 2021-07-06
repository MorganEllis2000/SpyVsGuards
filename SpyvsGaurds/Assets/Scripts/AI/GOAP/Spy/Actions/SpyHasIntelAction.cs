////////////////////////////////////////////////////////////
// File: <SpyHasIntelAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <checks to see if the spy has the intel and if it does then they can escape>
////////////////////////////////////////////////////////////

using Random = UnityEngine.Random;
using System;
using UnityEngine;

public class SpyHasIntelAction : GoapAction
{
	private bool hasIntel = false;

	public SpyHasIntelAction()
	{
		addEffect("getIntel", true);
	}


	public override void reset()
	{
		hasIntel = false;
		target = null;
	}

	public override bool isDone()
	{
		return hasIntel;
	}

	public override bool requiresInRange()
	{
		return false; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{

		IntelComponent goTotem = (IntelComponent)UnityEngine.GameObject.FindObjectOfType(typeof(IntelComponent));
		target = goTotem.gameObject;

		if (target == null)
		{
			return false;
		}
		if (target.GetComponent<Knowledge>().IntelCollected())
		{
			return true;
		}

		return false;
	}

	public override bool perform(GameObject agent)
	{
		hasIntel = true;
		return true;
	}

}

