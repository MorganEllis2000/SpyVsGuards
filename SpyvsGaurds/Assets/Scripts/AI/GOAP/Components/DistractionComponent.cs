using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionComponent : MonoBehaviour
{
    Knowledge knowledge;
    void Start()
    {
        knowledge = GameObject.FindGameObjectWithTag("Spy").GetComponent<Knowledge>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knowledge.spyDistracting == true)
		{
            Debug.Log("Distracting");
		}
    }
}
