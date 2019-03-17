using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCamera : MonoBehaviour {
    public float TurnSpeed = 5F;

    void Start () {
		
	}
	
	void Update () {
        float turn = Input.GetAxis("Mouse Y") * -TurnSpeed;
        transform.Rotate(turn, 0, 0);
    }
}
