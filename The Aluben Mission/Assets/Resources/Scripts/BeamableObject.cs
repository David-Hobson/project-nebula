using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamableObject : MonoBehaviour {

    private float charge;
    private bool beamed;

	// Use this for initialization
	void Start () {
        charge = 0;
        beamed = false;
	}
	
	// Update is called once per frame
	void Update () {

        CalculateCharge();
        Debug.Log(charge);

	}

    //Calculates the charge amount for the object
    private void CalculateCharge(){
        if (beamed) {
            charge += Time.deltaTime;
        } else {
            charge = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.name == "Beam"){
            beamed = true;
        }

    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Beam") {
            beamed = false;
        }
    }

}