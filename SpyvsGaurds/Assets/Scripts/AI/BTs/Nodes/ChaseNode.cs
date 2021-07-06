////////////////////////////////////////////////////////////
// File: <ChaseNode.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <If the spy has been spotted then the guard will chase the spy>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private Transform spy;
    private NavMeshAgent agent;
    private EnemyAI ai;
    private bool canAgentSeeSpy;
    private float speed = 3f;

    LineOfSightNode lineOfSightNode;
    Base_Spy base_Spy;
    Knowledge knowledge;


    public ChaseNode(Transform spy, NavMeshAgent agent, EnemyAI ai, bool canAgentSeeSpy)
    {
        this.spy = spy;
        this.agent = agent;
        this.ai = ai;
        this.canAgentSeeSpy = canAgentSeeSpy;
    }

    public override NodeState Evaluate()
    {
        Vector3 playerPosition = spy.transform.position;
        Vector3 vectorToPlayer = playerPosition - agent.transform.position;
        canAgentSeeSpy = true;
        base_Spy = GameObject.FindGameObjectWithTag("Spy").GetComponent<Base_Spy>();
        knowledge = GameObject.FindGameObjectWithTag("Spy").GetComponent<Knowledge>();
        if (canAgentSeeSpy == true && knowledge.spyHiding == false)
        {
            agent.velocity = vectorToPlayer.normalized * speed;
            agent.transform.LookAt(playerPosition);
            //base_Spy.interrupt = true;
            return NodeState.RUNNING;
        }
        else
        {
            knowledge.spyHiding = false;
            canAgentSeeSpy = false;
            return NodeState.FAILURE;
        }
    }
}