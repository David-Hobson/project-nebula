using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeypadConditions : MonoBehaviour {

	//2 Text Field Responses
	[SerializeField] private Text keypadReady;
	[SerializeField] private Text keypadNotReady;

	//Create 2 arrays for checking dynamic game conditions and a final variable to flip the keypad on
	GameObject[] switches;
	//GameObject test;
	//Door references to dynamically remove the boss door
	GameObject doorBot;
	GameObject doorTop;
	bool[] isOn;
	public bool keypadConditionsMet = false;

	// Use this for initialization
	void Start () {
		switches = new GameObject[5]; 
		isOn = new bool[5];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSwitches(){
		/*test = GameObject.Find ("Switch2");
		Debug.Log ("real?");
		bool yes = test.GetComponent<PuzzleSwitches> ().SwitchOn;
		Debug.Log (yes);
		Debug.Log ("heh");*/
		switches[0] = GameObject.Find ("Switch2");
		switches [1] = GameObject.Find ("Switch5");
		switches [2] = GameObject.Find ("Switch1");
		switches [3] = GameObject.Find ("Switch+");
		switches [4] = GameObject.Find ("Switch7");
	}

	//Displays keypad if all switches are on, otherwise displays alternative message
	public void OnTriggerEnter2D(Collider2D other) {
		
		doorBot = GameObject.Find ("doorBotR");
		doorTop = GameObject.Find ("doorTopR");

		if (other.gameObject.tag == "Player") {
			checkConditions ();
			if (keypadConditionsMet == true) {
				keypadReady.enabled = true;
				doorBot.GetComponent<SpriteRenderer> ().enabled = false; 
				doorTop.GetComponent<SpriteRenderer> ().enabled = false;
                SceneManager.LoadScene(2);
			} else {
				keypadNotReady.enabled = true;
			}
				
		}
			
	}

	//Removes text message once player leaves collider
	public void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			keypadReady.enabled = false;
			keypadNotReady.enabled = false;
		}
	}


	//Checks that all 5 switches have been flipped on
	public void checkConditions(){
		//Debug.Log ("fishy");
		setSwitches ();
		//Debug.Log ("more fishy fish");
		for(int i=0; i<5; i++){
			isOn[i] = switches [i].GetComponent<PuzzleSwitches> ().SwitchOn;
		}

		if (isOn [0] == true && isOn [1] == true && isOn [2] == true && isOn [3] == true && isOn [4] == true) {
			keypadConditionsMet = true;
		}
	}
}
