using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("BEAMING!!");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("ANYTHING");
    }
}
