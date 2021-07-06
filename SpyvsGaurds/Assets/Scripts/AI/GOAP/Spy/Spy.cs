////////////////////////////////////////////////////////////
// File: <Spy.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <Inherits from the Base_Spy class and is responsible for creating a plan for the spy to execute>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : Base_Spy
{
	public GameObject[] SearchPoints;
	public GameObject currentSearchSpot;

	public bool Hide;

	public override HashSet<KeyValuePair<string, object>> createGoalState()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

		goal.Add(new KeyValuePair<string, object>("Escape", true));
		goal.Add(new KeyValuePair<string, object>("avoidGuard", true));
		goal.Add(new KeyValuePair<string, object>("searchArea", true));
		goal.Add(new KeyValuePair<string, object>("getIntel", true));

		return goal;
	}
	private void Start()
	{
		int index = Random.Range(0, SearchPoints.Length);
		currentSearchSpot = new GameObject("Search Spot");
		currentSearchSpot = SearchPoints[index];
	}

	public GameObject GetCurrentSeachSpot()
	{
		return currentSearchSpot;
	}

	public GameObject SetSearchSpot()
	{
		int index = Random.Range(0, SearchPoints.Length);
		currentSearchSpot = SearchPoints[index];
		return currentSearchSpot;
	}

	public void ChangeTarget(Vector3 position)
	{
		currentSearchSpot.transform.position = position;
	}

	public bool GetNeedsToHide()
	{
		return Hide;
	}

	public void SetNeedsToHide(bool hide)
	{
		Hide = hide;
		GetComponent<Base_Spy>().interrupt = true;
	}
}
