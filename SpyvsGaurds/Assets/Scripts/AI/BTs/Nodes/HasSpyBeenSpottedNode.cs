////////////////////////////////////////////////////////////
// File: <HasSpyBeenSpottedNode.cs>
// Author: <Morgan Ellis>
// Date Created: <21/11/2020>
// Brief: <Checks to see if the spy has been spotted and is they have then a search needs to start>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasSpyBeenSpottedNode : Node
{
	private BlackBoard blackboard;
	private bool canAgentSeeSpy;
	private bool startSearch;

	public HasSpyBeenSpottedNode(bool canAgentSeeSpy, bool startSearch)
	{
		this.canAgentSeeSpy = canAgentSeeSpy;
	}

	public override NodeState Evaluate()
	{
		blackboard = GameObject.Find("Blackboard").GetComponent<BlackBoard>();

		if (blackboard.wasSpyFound == true)
		{
			startSearch = true;
			Debug.Log("Spy Spotted");
			return NodeState.SUCCESS;
		}
		else
		{
			return NodeState.FAILURE;
		}

	}
}
