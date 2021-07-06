////////////////////////////////////////////////////////////
// File: <SpyGetIntelAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the spy does not have the intel it needs to check for its location and go and get it>
////////////////////////////////////////////////////////////

using Random = UnityEngine.Random;
using System;
using UnityEngine;

public class SpyGetIntelAction : GoapAction
{
	private bool getIntel = false;

	public SpyGetIntelAction()
	{
		addEffect("getIntel", true);
		addPrecondition("intelClearOfEnemies", true);
	}


	public override void reset()
	{
		target = null;
	}

	public override bool isDone()
	{
		return getIntel;
	}

	public override bool requiresInRange()
	{
		return true; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{

		IntelComponent goTotem = (IntelComponent)UnityEngine.GameObject.FindObjectOfType(typeof(IntelComponent));
		target = goTotem.gameObject;

		if (target == null)
		{
			return false;
		}
		if (!target.GetComponent<Knowledge>().IntelCollected())
		{
			return true;
		}

		return false;
	}

	public override bool perform(GameObject agent)
	{
		getIntel = true;
		return true;
	}

}

