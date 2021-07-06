////////////////////////////////////////////////////////////
// File: <SpyLineOfSight.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This allows the spy to see guards and choose to interrput the plan to hide or distract the enemy>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpyLineOfSight : MonoBehaviour
{

    public bool canSeeGuard;
    public bool canSeeIntel;

    public List<Transform> targetsInSight = new List<Transform>();

	public LayerMask targetMask;
	public LayerMask obsticleMask;
	float raycastRange = 10f;
	float visionConeAngle = 120f;

	GameObject[] guardsList;
	private Base_Spy base_Spy;

	// Start is called before the first frame update
	void Start()
    {
		base_Spy = GameObject.FindGameObjectWithTag("Spy").GetComponent<Base_Spy>();
	}

    // Update is called once per frame
    void Update()
    {
		LineOfSight();
    }

	private void LineOfSight()
	{
		targetsInSight.Clear();
		canSeeGuard = false;
		canSeeIntel = false;

		guardsList = GameObject.FindGameObjectsWithTag("Guard");

		Vector3 playerPosition = transform.position;
		Vector3 vectorToPlayer = GetComponent<NavMeshAgent>().transform.position - playerPosition;
		RaycastHit hit;
		Ray ray;
		for (int i = 0; i < guardsList.Length; i++)
		{
			ray = new Ray(transform.forward, guardsList[i].transform.position - transform.forward);

			if (canSeeGuard == true && Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.CompareTag("Guard") && !hit.collider.gameObject.CompareTag("Wall"))
				{
					canSeeGuard = true;
					base_Spy.interrupt = true;
					Debug.Log("Guard");
				}
				else if (!hit.collider.gameObject.CompareTag("Guard") && !hit.collider.gameObject.CompareTag("Wall") || !hit.collider.gameObject.CompareTag("Guard"))
				{
					//chooseRandomPoints = true;
					canSeeGuard = false;
					base_Spy.interrupt = false;
				}
			}
			else if (canSeeGuard == false && Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.CompareTag("Guard") && !hit.collider.gameObject.CompareTag("Wall") && Vector3.Distance(guardsList[i].transform.position, playerPosition) < raycastRange &&
					Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
				{
					canSeeGuard = true;
					base_Spy.interrupt = true;
				}
			}
		}
	}
}
