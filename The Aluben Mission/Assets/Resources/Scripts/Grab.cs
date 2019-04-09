using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grab : MonoBehaviour {

	//Variables to track relic being held 
	private bool isHolding1; 
	private bool isHolding2; 

	//Player1 and Player2 gameobjects to track collisions
	private GameObject player1;
	private GameObject player2;

	//P1 and P2 controller objects to manipulate speed and firing need to added in inspector
	public PlayerController P1;
	public PlayerController P2;

	//Initial relic position for resets and completion boolean
	private Vector3 initalPos;
	private bool complete;

	//Initialize required objects and variables
	void Start() {
		player1 = GameObject.Find ("Player 1");
		player2 = GameObject.Find ("Player 2");
		isHolding1 = false;
		isHolding2 = false;
		initalPos = this.transform.position;
		complete = false;
	}

	//Update tracking of relic and who is holding it 
	void Update() {
		completePuzzle ();

		//freeze relic in place one it has reached it's final destination
		if (complete) {
            SceneManager.LoadScene("LoadingScene2");
		} 
		//Check which player is holding the relic and prohibit their respective shooting and apply speed debuff
		else {
			//P1
			if (player1.GetComponent<PlayerController>().GetIsHolding()) {
				this.transform.position = player1.transform.position;
				player1.GetComponent<PlayerController> ().SetSpeed (0.5f);
            } else{
                player1.GetComponent<PlayerController>().SetSpeed(1.0f);
            } 

		    if (player2.GetComponent<PlayerController>().GetIsHolding()) {
				this.transform.position = player2.transform.position;
				player2.GetComponent<PlayerController> ().SetSpeed (0.5f);
            } else {
                player2.GetComponent<PlayerController>().SetSpeed(1.0f);
            }
        }

	}

	//Track collision with the relic for both players
	private void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {

			//If player1 presses X while colliding then pickup the object
			if (Input.GetButtonDown ("P1X")) {
				player1.GetComponent<PlayerController> ().SetSpeed(1);
				player1.GetComponent<PlayerController> ().ToggleIsHolding();

			} 
			//If player2 presses X while colliding then pickup the object
			else if (Input.GetButtonDown ("P2X")) {
				player2.GetComponent<PlayerController> ().SetSpeed(1);
				player2.GetComponent<PlayerController> ().ToggleIsHolding();
			}

		} else if (other.tag == "EnemyBullet" || other.tag == "Enemy") {
			this.transform.position = initalPos;
		}
	}

	//Detect when the relic has been placed back into its correct location ugly right now, box collider was being finicky
	private void completePuzzle(){
		if(-0.25f <= this.transform.position.x && this.transform.position.x  <= -0.22f && -0.1f <= this.transform.position.y  && this.transform.position.y <= -0.08f && this.transform.position.z == 0){
			complete = true;

		}
	}


}