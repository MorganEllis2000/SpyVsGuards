////////////////////////////////////////////////////////////
// File: <EnemyAi.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This is the main file for the guard that controls the behvaiour tree and therefor the actions on the guards>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private bool canAgentSeeSpy;
    private bool startSearch;
    [SerializeField] private bool chooseRandomPoints;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Transform> patrolSpots;
    [SerializeField] private Waypoints[] waypoints;
    [SerializeField] private Light visionCone;

    [SerializeField] private Animator lookAroundAnim;

    private Material material;
    private NavMeshAgent agent;

    private Transform waypoint;
    private Transform resetWaypoint;

    private Node topNode;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this, canAgentSeeSpy);
        PatrolNode patrolNode = new PatrolNode(patrolSpots, agent, this);
        LineOfSightNode lineOfSightNode = new LineOfSightNode(playerTransform, this, agent, visionCone, canAgentSeeSpy, chooseRandomPoints);
        HasSpyBeenSpottedNode hasSpyBeenSpottedNode = new HasSpyBeenSpottedNode(canAgentSeeSpy, startSearch);
        FindClosestWaypointNode findClosestWaypointNode = new FindClosestWaypointNode(waypoints, playerTransform, this, startSearch);
        GoToClosestWaypointNode goToClosestWaypointNode = new GoToClosestWaypointNode(agent, this, startSearch);
        DistractionNode distractionNode = new DistractionNode(agent, this);

        Sequence chaseSequence = new Sequence(new List<Node> { lineOfSightNode, chaseNode });
        Selector patrolSelector = new Selector(new List<Node> { chaseSequence, distractionNode, patrolNode });
        Sequence searchSequence = new Sequence(new List<Node> { hasSpyBeenSpottedNode, findClosestWaypointNode, goToClosestWaypointNode });

        topNode = new Selector(new List<Node> { chaseSequence, searchSequence, patrolSelector });
    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.red);
            agent.isStopped = true;
        }
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

    public Transform GetWaypoint()
    {
        return waypoint;
    }

    public void SetWaypoint(Transform waypoint)
    {
        this.waypoint = waypoint;
    }

    public void ResetWaypoint()
    {
        waypoint = resetWaypoint;
    }
}

