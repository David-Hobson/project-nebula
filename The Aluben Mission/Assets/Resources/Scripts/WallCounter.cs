using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCounter : MonoBehaviour {

	private GameObject slimeWall;

	private int enemyCounter;
	private int stageOneEnemyCounter;

	// Use this for initialization
	void Start () {
		slimeWall = GameObject.Find ("SlimeWall");

		//Check the number of enemies on the map
		print (GameObject.FindGameObjectsWithTag ("Enemy").Length);
		enemyCounter = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		//set number of enemies to be defeated until wall is destroyed
		stageOneEnemyCounter = enemyCounter - 6;
		print (stageOneEnemyCounter);
	}
	
	// Update is called once per frame
	void Update () {
		enemyCounter = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		//Destroy Wall if enemy counter equals stage one requirement else return a counter (add to HUD)
		if (enemyCounter == stageOneEnemyCounter) {
			Destroy (slimeWall);
		} 
		else { 
			print ("you have " + GameObject.FindGameObjectsWithTag ("Enemy").Length + " enemies remaining");
			print (stageOneEnemyCounter + " " + GameObject.FindGameObjectsWithTag ("Enemy").Length);
		}
	}
}
