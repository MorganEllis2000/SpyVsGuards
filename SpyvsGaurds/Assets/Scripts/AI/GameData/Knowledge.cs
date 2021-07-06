using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
	public bool gotIntel = false;
	private bool intelCollected = false;
	public bool intelSpotted = false;
	public bool enemyNearIntel = false;
	public bool Escaped;
	public bool move;
	public GameObject currrentDistraction;
	public bool spyHiding;
	public bool spyDistracting;

	public bool IntelCollected()
	{
		return intelCollected;
	}

	public bool HasIntelBeenSpotted()
	{
		return intelSpotted;
	}

	public bool EnemyNearIntel()
	{
		return enemyNearIntel;
	}
}
