using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPieceController : MonoBehaviour {
    public Collider Collider;
    public bool collected;
    // Use this for initialization
    void Start () {
        Collider = GetComponent<Collider>();
        collected = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            collected = true;
        }
        else
        {
            Physics.IgnoreCollision(collision.collider, Collider);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
