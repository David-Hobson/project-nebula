using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {
	private GameObject relic;
	private GameObject falseWall;

	// Use this for initialization
	void Start () {
		relic = GameObject.Find ("moveable1");
		falseWall = GameObject.Find ("FalseWall");

	}
	
	// Update is called once per frame
	void Update () {
		
		//bool check = relic.GetComponent<DualGrab> ().GetOpen ();
		//print (check);	
		//print (this.gameObject.layer);
		if (relic.GetComponent<GoalLinePuzzle> ().GetOpen () == true) {
			Destroy (falseWall);
		}

			
	}
}
