using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vect = player.position - transform.position;
        if (vect.magnitude <= 0.1){
            print("TRIGGER");
        }
	}
}
