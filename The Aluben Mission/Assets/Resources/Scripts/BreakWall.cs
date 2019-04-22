using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {
	private GameObject relic;
	private GameObject falseWall;

	private AudioClip breaking;
	private bool played;

	// Use this for initialization
	void Start () {
		relic = GameObject.Find ("moveable1");
		falseWall = GameObject.Find ("FalseWall");
		breaking = falseWall.GetComponent<AudioSource> ().clip;
		played = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		//bool check = relic.GetComponent<DualGrab> ().GetOpen ();
		//print (check);	
		//print (this.gameObject.layer);
		if (relic.GetComponent<GoalLinePuzzle> ().GetOpen () == true) {
			if (played == false) {
				falseWall.GetComponent<AudioSource> ().PlayOneShot (breaking, 1);
				played = true;
			}
			Invoke ("breakWall", 2);
		}

			
	}

	private void breakWall(){
		Destroy (falseWall);
	}
}
