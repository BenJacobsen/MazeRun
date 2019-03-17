using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    public bool Locked;

    void Start () {
        Locked = false;
        UpdateCursor();
    }
	
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            Locked = !Locked;
            UpdateCursor();
        }
    }
    void UpdateCursor ()
    {
        if (Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
