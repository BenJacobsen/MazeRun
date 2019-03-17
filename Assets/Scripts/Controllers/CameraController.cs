using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera MenuCamera;
    private Camera MazeCamera;
    private GameObject MenuController;

    void Start () {
        MenuController = GameObject.Find("MenuController");
        MenuCamera = GameObject.Find("MenuCamera").GetComponent<Camera>();
        MazeCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    }
	
	void Update () {
		if (MenuController.GetComponent<MenuController>().CurrentMenu == Menu.None || 
            MenuController.GetComponent<MenuController>().CurrentMenu == Menu.Pause ||
            MenuController.GetComponent<MenuController>().CurrentMenu == Menu.InGameSettings)
        {
            MenuCamera.enabled = false;
            if (GameObject.Find("PlayerCamera").GetComponent<Camera>() != null)
            {
                MazeCamera.enabled = true;
            }
            MenuCamera.enabled = true;
        }
        else
        {
            if (GameObject.Find("PlayerCamera").GetComponent<Camera>() != null)
            {
                MazeCamera.enabled = false;
            }
            MenuCamera.enabled = true;
        }
	}
}
