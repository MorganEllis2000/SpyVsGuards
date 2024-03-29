﻿////////////////////////////////////////////////////////////
// File: <BlackBoard.cs>
// Author: <Morgan Ellis>
// Date Created: <16/11/2020>
// Brief: <Responsible for keeping track of the state of the guards, such as whether they have found the spy and the last known position of the spy>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlertState
{
    Low,
    Medium,
    High,
}

public class BlackBoard : MonoBehaviour
{
    public bool spyBeenSpotted;
    public Vector3 lastKnownPosition;
    public bool chooseRandomSpots;
    public bool wasSpyFound;

    public AlertState _alertState;

    void Start()
    {
        spyBeenSpotted = false;
        chooseRandomSpots = false;
    }


}
