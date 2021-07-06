////////////////////////////////////////////////////////////
// File: <Base_Spy.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This is the base class that the spy class will inherit from>
////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public abstract class Base_Spy : MonoBehaviour, IGoap
{
	public Knowledge knowledge;
	public float moveSpeed = 1;
	private NavMeshAgent spy;
	public bool interrupt;

	void Start()
	{
		if (knowledge == null)
		{
			knowledge = gameObject.AddComponent<Knowledge>() as Knowledge;
		}

	}


	void Update ()
	{

	}

	public HashSet<KeyValuePair<string,object>> getWorldState () {
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();

		worldData.Add(new KeyValuePair<string, object>("getIntel", false));
		worldData.Add(new KeyValuePair<string, object>("intelClearOfEnemies", (GetComponent<Knowledge>().enemyNearIntel)));
		worldData.Add(new KeyValuePair<string, object>("searchArea", false));
		worldData.Add(new KeyValuePair<string, object>("Escape", false));
		worldData.Add(new KeyValuePair<string, object>("avoidGuard", (GetComponent<SpyLineOfSight>().canSeeGuard)));

		return worldData;
	}

	public abstract HashSet<KeyValuePair<string,object>> createGoalState ();


	public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal)
	{

	}

	public void planFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
	{
		Debug.Log ("<color=green>Plan found</color> "+GoapAgent.prettyPrint(actions));
	}

	public void actionsFinished ()
	{
		Debug.Log ("<color=blue>Actions completed</color>");
	}

	public void planAborted (GoapAction aborter)
	{
		Debug.Log ("<color=red>Plan Aborted</color> "+GoapAgent.prettyPrint(aborter));
		GetComponent<GoapAgent>().GetDataProvider().AllActionsFinished();
		aborter.reset();
		aborter.doReset();
	}

	public void AllActionsFinished()
	{

	}

	public void AbortPlan(GoapAction failedAction)
	{
		GetComponent<GoapAgent>().GetDataProvider().AllActionsFinished();
		failedAction.reset();
		failedAction.doReset();
	
	}

	public bool moveAgent(GoapAction nextAction) {
		float distance = Vector3.Distance(transform.position, nextAction.target.transform.position); //Get distance to target


		if (distance < 1000f)//If it is in range
		{
			GetComponent<NavMeshAgent>().speed = moveSpeed;
			GetComponent<NavMeshAgent>().SetDestination(nextAction.target.transform.position);
		}

		if (interrupt)
		{
			GetComponent<GoapAgent>().GetDataProvider().planAborted(nextAction);
			planAborted(nextAction);
			interrupt = false;

			return true;
		}

		if ( distance <= 0.1f ) {
			// we are at the target location, we are done
			nextAction.setInRange(true);
			return true;
		}
		else
		{
			return false;
		}


	}
}

