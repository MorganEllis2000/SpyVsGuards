using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPoints : MonoBehaviour
{
    public GameObject[] searchPoints;
    GameObject searchPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomSearchPoint()
	{
        return searchPoint;
	}

    public GameObject SetRandomSearchPoint()
	{
        int index = Random.Range(0, searchPoints.Length);
        searchPoint = searchPoints[index];
        return searchPoint;
    }
}
