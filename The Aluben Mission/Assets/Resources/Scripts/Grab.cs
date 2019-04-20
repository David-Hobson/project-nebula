using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grab : MonoBehaviour {

	//Variables to track relic being held 
	private bool redRelic; 
	private bool blueRelic; 

	//Player1 and Player2 gameobjects to track collisions
	private GameObject player1;
	private GameObject player2;

	//Initial relic position for resets and completion boolean
	private Vector3 initalPos;
	private bool complete;

	//Initialize required objects and variables
	void Start() {
		player1 = GameObject.Find ("Player 1");
		player2 = GameObject.Find ("Player 2");
		redRelic = false;
		blueRelic = true;
		initalPos = this.transform.position;
		complete = false;
	}

	//Update tracking of relic and who is holding it 
	void Update() {
		CompletePuzzle ();
		RelicColour ();

		//freeze relic in place one it has reached it's final destination
		if (complete) {
            SceneManager.LoadScene("LoadingScene2");
		} 
		//Check which player is holding the relic and prohibit their respective shooting and apply speed debuff
		else {
			//P1
			if (player1.GetComponent<PlayerController>().GetIsHolding()) {
				this.transform.position = player1.transform.position;
				if (blueRelic) {
					player1.GetComponent<PlayerController> ().SetSpeed (0.75f);
				} else {
					player1.GetComponent<PlayerController> ().SetSpeed (0.25f);
				}
            } else{
                player1.GetComponent<PlayerController>().SetSpeed(1.0f);
            } 


		    if (player2.GetComponent<PlayerController>().GetIsHolding()) {
				this.transform.position = player2.transform.position;
				if (redRelic) {
					player2.GetComponent<PlayerController> ().SetSpeed (0.75f);
				} else {
					player2.GetComponent<PlayerController> ().SetSpeed (0.25f);
				}
            } else {
                player2.GetComponent<PlayerController>().SetSpeed(1.0f);
            }
        }

	}

	//Track collision with the relic for both players
	private void OnTriggerStay2D(Collider2D other){

		if (other.name == "Player 1") {
			
			//If player1 presses X while colliding then pickup the object
			if (player1.GetComponent<PlayerController> ().GetInteraction ()) {
				print ("player 1 input");
				player1.GetComponent<PlayerController> ().SetSpeed (1);
				player1.GetComponent<PlayerController> ().ToggleIsHolding ();

			} 
				
		} else if (other.name == "Player 2") {
			//If player2 presses X while colliding then pickup the object
			if (player2.GetComponent<PlayerController> ().GetInteraction ()) {
				print ("player 2 input");
				player2.GetComponent<PlayerController> ().SetSpeed (1);
				player2.GetComponent<PlayerController> ().ToggleIsHolding ();
			}
		} else if (other.tag == "EnemyBullet" || other.tag == "Enemy") {
			this.transform.position = initalPos;
		}
			
	}
		
	//Requirement: F-31,51,52,53
	//Detect when the relic has been placed back into its correct location ugly right now, box collider was being finicky
	private void CompletePuzzle(){
		if(-0.25f <= this.transform.position.x && this.transform.position.x  <= -0.22f && -0.1f <= this.transform.position.y  && this.transform.position.y <= -0.08f && this.transform.position.z == 0){
			complete = true;

		}
	}

	public void FlipColour(){
		redRelic = !redRelic;
		blueRelic = !blueRelic;
	}

	public void RelicColour(){
		if (redRelic) {
			this.GetComponent<SpriteRenderer> ().color = Color.red;
		} else if (blueRelic) {
			this.GetComponent<SpriteRenderer> ().color = Color.blue;
		}
	}

	public void endTriggerColour(){
		redRelic = false;
		blueRelic = false;

		this.GetComponent<SpriteRenderer> ().color = Color.white;
	}

}
