using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterWall : MonoBehaviour {
	
	public GameObject wall;
	private int stageOneEnemyCounter;

	private int enemyCounter;
	/*
	// Use this for initialization
	void Start () {
		enemyCounter = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		print (GameObject.FindGameObjectsWithTag ("Enemy").Length);
	}
	
	// Update is called once per frame
	void Update () {
		enemyCounter = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		//Destroy Wall if enemy counter equals stage one requirement else return a counter (add to HUD)
		if (enemyCounter == stageOneEnemyCounter) {
			Destroy (wall);
		} 
		else { 
			print ("you have " + GameObject.FindGameObjectsWithTag ("Enemy").Length + " enemies remaining");
			print (stageOneEnemyCounter + " " + GameObject.FindGameObjectsWithTag ("Enemy").Length);
		}
	}
	*/

	void Start(){
		
	}

	void Update(){
		enemyCounter = wall.transform.childCount;
		BreakWall ();
	}

	public void BreakWall(){
		if (enemyCounter == 0) {
			Destroy (wall);
		}
	}

}

