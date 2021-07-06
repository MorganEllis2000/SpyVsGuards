////////////////////////////////////////////////////////////
// File: <LineOfSightNode.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This allows the guard to see, if the spy is within a certain range than the spy is seen, else the guard continues to patrol or search>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineOfSightNode : Node
{
	private Transform spy;
	private EnemyAI ai;
	private Light visionCone;
	private NavMeshAgent agent;

	private bool canAgentSeeSpy;
	private bool startSearch;

	private float raycastRange = 20f;
	private float visionConeAngle = 120;

	private float timer;
	private float timerMax;
	private float totalTime;

	private BlackBoard blackboard;
	private Knowledge knowledge;
	private Base_Spy base_Spy;

	public LineOfSightNode(Transform spy, EnemyAI ai, NavMeshAgent agent, Light visionCone, bool canAgentSeeSpy, bool startSearch)
	{
		this.spy = spy;
		this.ai = ai;
		this.agent = agent;
		this.visionCone = visionCone;
		this.canAgentSeeSpy = canAgentSeeSpy;
		this.startSearch = startSearch;

	}

	public override NodeState Evaluate()
	{
		knowledge = GameObject.FindGameObjectWithTag("Spy").GetComponent<Knowledge>();
		base_Spy = GameObject.FindGameObjectWithTag("Spy").GetComponent<Base_Spy>();
		blackboard = GameObject.Find("Blackboard").GetComponent<BlackBoard>();
		CanSpyBeSeen();
		if (canAgentSeeSpy == true && knowledge.spyHiding == false)
		{
			ai.SetColor(Color.white);
			//Debug.Log(canAgentSeeSpy);
			//base_Spy.interrupt = true;
			//Debug.Log("Interrupt:" + base_Spy.interrupt);
			return NodeState.SUCCESS;
		}
		knowledge.spyHiding = false;
		canAgentSeeSpy = false;
		return NodeState.FAILURE;
	}

	private void CanSpyBeSeen()
	{

		Vector3 playerPosition = spy.transform.position;
		Vector3 vectorToPlayer = playerPosition - agent.transform.position;
		RaycastHit hit;
		Ray ray = new Ray(agent.transform.forward, GameObject.FindGameObjectWithTag("Spy").transform.position - agent.transform.forward);

		if (canAgentSeeSpy == true && Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.CompareTag("Spy") && !hit.collider.gameObject.CompareTag("Wall"))
			{
				canAgentSeeSpy = true;
				//chooseRandomPoints = false;
			}
			else if (!hit.collider.gameObject.CompareTag("Spy") && !hit.collider.gameObject.CompareTag("Wall") || !hit.collider.gameObject.CompareTag("Spy"))
			{
				//chooseRandomPoints = true;
				canAgentSeeSpy = false;
				blackboard.lastKnownPosition = playerPosition;
				Debug.Log("Last Known Position: " + blackboard.lastKnownPosition);
				visionCone.color = Color.white;
			}
		}
		else if (canAgentSeeSpy == false && Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject.CompareTag("Spy") && !hit.collider.gameObject.CompareTag("Wall") &&  Vector3.Distance(ai.transform.position, playerPosition) < raycastRange &&
			Vector3.Angle(agent.transform.forward, vectorToPlayer) <= visionConeAngle)
			{
				visionCone.color = Color.yellow;
				totalTime += Time.deltaTime;
				if (totalTime >= 2f)
				{
					visionCone.color = Color.red;
					Debug.DrawRay(agent.transform.position, agent.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
					canAgentSeeSpy = true;
					blackboard.spyBeenSpotted = true;
					totalTime = 0;
				}
			}
			else if (hit.collider.gameObject.CompareTag("Spy") && !hit.collider.gameObject.CompareTag("Wall") && Vector3.Distance(ai.transform.position, playerPosition) < 10 &&
			Vector3.Angle(agent.transform.forward, vectorToPlayer) <= 90)
			{
				visionCone.color = Color.yellow;
				totalTime += Time.deltaTime;
				if (totalTime >= 1f)
				{
					visionCone.color = Color.red;
					Debug.DrawRay(agent.transform.position, agent.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
					canAgentSeeSpy = true;
					blackboard.spyBeenSpotted = true;
					totalTime = 0;
				}
			}
			else if (hit.collider.gameObject.CompareTag("Spy") && !hit.collider.gameObject.CompareTag("Wall") && Vector3.Distance(ai.transform.position, playerPosition) < 1 &&
			Vector3.Angle(agent.transform.forward, vectorToPlayer) <= 360)
			{
				visionCone.color = Color.yellow;
				totalTime += Time.deltaTime;
				if (totalTime >= 0f)
				{
					visionCone.color = Color.red;
					Debug.DrawRay(agent.transform.position, agent.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
					canAgentSeeSpy = true;
					blackboard.spyBeenSpotted = true;
					totalTime = 0;
				}
			}
			else
			{
				visionCone.color = Color.white;
				canAgentSeeSpy = false;
			}
		}
	}
}
