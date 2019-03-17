using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPieceController : MonoBehaviour {
    public Collider Collider;
    public bool collected;
    private GameObject Player;
    // Use this for initialization
    void Start () {
        Collider = GetComponent<Collider>();
        Player = GameObject.Find("Player");
        collected = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            collected = true;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
