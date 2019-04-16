using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamableObject : MonoBehaviour {

    private float charge;
    private bool beamed;

    //private GameObject energize;

	// Use this for initialization
	void Start () {
        charge = 0;
        beamed = false;
        //energize = this.transform.GetChild(0).gameObject;
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
            //this.EnergizeOrb(beamed);
        } else {
            charge = 0;
            //this.EnergizeOrb(beamed);
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

    //private void EnergizeOrb(bool energized){
    //    energize.GetComponent<SpriteRenderer>().enabled = energized;
    //}

}