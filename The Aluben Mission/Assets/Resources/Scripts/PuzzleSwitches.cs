using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitches : MonoBehaviour {

	//variable to check if switch has been tripped and variable for audiosource
	public bool SwitchOn = false;
	public AudioSource activatedSwitch;

	void Start () {

		//get the audio file currently loaded into the switch
		activatedSwitch = GetComponent<AudioSource> ();
	}

	void Update () {

	}

	//Checks that the player has hit the switch then plays audio queue and sets switchOn to True
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			activatedSwitch.Play();
			this.SwitchOn = true;
		}
	}
}
