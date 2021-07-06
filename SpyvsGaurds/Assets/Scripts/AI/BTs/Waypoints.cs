////////////////////////////////////////////////////////////
// File: <Waypoints.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This is responsible for storing the points that the guards can search when they loose sight of the player>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
	[SerializeField] private Transform[] waypoints;

	public Transform[] GetWaypoints()
	{
		return waypoints;
	}
}