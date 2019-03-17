using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MonoBehaviour {
    public enum AIState { GoToWaypoint, SeePlayer }
    public AIState currentState;
    public Cell waypoint;

    // Use this for initialization
    void Start () {
        currentState = AIState.GoToWaypoint;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
            case AIState.GoToWaypoint:
                
        }
	}

    void getDirection
}
