using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterWall : MonoBehaviour {
	
	public GameObject wall;
	private int stageOneEnemyCounter;

	private int enemyCounter;
	private AudioClip breaking;

	private bool played;
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
		breaking = wall.GetComponent<AudioSource> ().clip;
		played = false;
	}

	void Update(){
		enemyCounter = wall.transform.childCount;
		EnemiesDefeated ();
	}

	public void EnemiesDefeated(){
		if (enemyCounter == 0) {
			if (played == false) {
				wall.GetComponent<AudioSource> ().PlayOneShot (breaking, 1);
				played = true;
			}
			Invoke ("breakWall", 2);
		}
	}

	private void breakWall(){
		Destroy (wall);
	}

}

