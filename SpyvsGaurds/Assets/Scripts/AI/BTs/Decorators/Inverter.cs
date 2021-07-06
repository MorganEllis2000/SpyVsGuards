////////////////////////////////////////////////////////////
// File: <Inverter.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <Inverts the outcome, if the outcome is false than it is inverted to true>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
	protected Node node;

	public Inverter(Node node)
	{
		this.node = node; 
	}

	public override NodeState Evaluate()
	{
		switch (node.Evaluate())
		{
			case NodeState.RUNNING:
				_nodeState = NodeState.RUNNING; 
				break;
			case NodeState.SUCCESS:
				_nodeState = NodeState.FAILURE; 
				break;
			case NodeState.FAILURE:
				_nodeState = NodeState.SUCCESS;
				break;
			default:
				break;
		}
		return _nodeState;
	}
}
