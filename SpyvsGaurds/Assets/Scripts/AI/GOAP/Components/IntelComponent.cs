using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class IntelComponent : MonoBehaviour
{
	public float GroundDistance = 2f; //radius
	public LayerMask targetMask;
	public float walkRadius = 100f;
	public GameObject[] intelSpawns;
	private Vector3 intelPosition;

	void Start()
	{
		int index = Random.Range(0, intelSpawns.Length);
		intelPosition = intelSpawns[index].transform.position;
		transform.position = intelPosition;


	}
	void Update()
	{
		bool enemyNear = Physics.CheckSphere(transform.position, GroundDistance, targetMask);
		if (enemyNear)
		{
			Debug.Log("Enemy Near");
		}
	}
}
