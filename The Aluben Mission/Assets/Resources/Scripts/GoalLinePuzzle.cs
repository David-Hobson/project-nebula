using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLinePuzzle : MonoBehaviour {

	//Player1 and Player2 gameobjects to track collisions
	private GameObject player1;
	private GameObject player2;

	//P1 and P2 controller objects to manipulate speed and firing need to added in inspector
	public PlayerController P1;
	public PlayerController P2;

	//Initial relic position for resets, goalLine for relic and completion boolean
	private Vector3 initalPos;
	private Vector3 goalLine;
	private bool complete;
	public bool openSesame;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("Player 1");
		player2 = GameObject.Find ("Player 2");
		initalPos = this.transform.position;
		goalLine = new Vector3 (-8, 0, 0);
		complete = false;
		openSesame = false;
	}
	
	// Update is called once per frame
	void Update () {
		CompletePuzzle ();

		//freeze relic in place one it has reached it's final destination
		if (complete) {
			openSesame = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "P1Projectile(Clone)" && player1.transform.position.x > this.GetComponent<BoxCollider2D>().offset.x) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (-1, 0, 0) * 0.5f;
		} 

		else if (other.gameObject.name == "P1Projectile(Clone)" && player1.transform.position.x < this.GetComponent<BoxCollider2D>().offset.x) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (1, 0, 0) * 0.5f;
		}

		//else if (this.transform.position.x > initalPos.x) {
		//	this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
		//} 

		else if (other.gameObject.name != "P2Projectile(Clone)") {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			this.transform.position = initalPos;
		} 

		else if (this.transform.position.x <= goalLine.x) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			//print ("yEs");
			complete = true;
		}
			
		else if (other.gameObject.name == "P2Projectile(Clone)" && player2.transform.position.y > this.GetComponent<BoxCollider2D>().offset.y) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -1, 0) * 0.5f;
		} 

		else if (other.gameObject.name == "P2Projectile(Clone)" && player2.transform.position.y < this.GetComponent<BoxCollider2D>().offset.y) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 1, 0) * 0.5f;
		}
	}

	private void CompletePuzzle(){
		if (this.transform.position.x <= goalLine.x) {
			complete = true;
			this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
		}
	}

	public bool GetOpen(){
		return openSesame;
	}
}
