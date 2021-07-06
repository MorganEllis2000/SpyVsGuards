
using System;
using UnityEngine;

public class IntelClearAction : GoapAction
{
	private bool intelClear = false;

	[SerializeField]
	private LayerMask targetMask;

	private float radius = 1f;



	public IntelClearAction()
	{
		addEffect("IntelClearOfEnemies", true);
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
		return false; 
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		IntelComponent goIntel = (IntelComponent)UnityEngine.GameObject.FindObjectOfType(typeof(IntelComponent));

		if (goIntel == null)
		{
			return false;
		}
		if (!GetComponent<SpyLineOfSight>().canSeeIntel == true)
		{
			return false;
		}
		Collider[] targetInRadius = Physics.OverlapSphere(goIntel.transform.position, radius, targetMask);//Get colliders in radius that we are interested in
		foreach (Collider collider in targetInRadius)
		{
			if (collider.CompareTag("Guard"))
			{
				Debug.Log("Guard in range");
				return false;
			}
		}
		return true;
	}

	public override bool perform(GameObject agent)
	{
		intelClear = true;
		return true;
	}

}

