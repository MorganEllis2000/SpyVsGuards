////////////////////////////////////////////////////////////
// File: <SpyIntelClearAction.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <Checks to see if the intel is clear and if not a distraction needs to be made>
////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using UnityEngine.AI;

public class SpyIntelClearAction : GoapAction
{
	private bool intelClear = false;


	[SerializeField]
	private float viewRadius;

	[SerializeField]
	private LayerMask targetMask;

	public SpyIntelClearAction()
	{
		addEffect("intelClearOfEnemies", true);
	}


	public override void reset()
	{
		intelClear = false;
		target = null;
	}

	public override bool isDone()
	{
		return intelClear;
	}

	public override bool requiresInRange()
	{
		return false; // yes we need to be near a chopping block
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		IntelComponent intel = GameObject.FindObjectOfType<IntelComponent>();
		if (intel == null)
		{
			return false;
		}
		if (!intel.GetComponent<Knowledge>().HasIntelBeenSpotted())
		{
			return false;
		}
		Collider[] targetsInView = Physics.OverlapSphere(intel.transform.position, viewRadius, targetMask);//Get colliders in radius that we are interested in
		foreach (Collider collider in targetsInView)
		{
			if (collider.CompareTag("Guard"))
			{
				return false;
			}
		}

		return target != null;
	}

	public override bool perform(GameObject agent)
	{
		intelClear = true;
		return true;
	}

}

