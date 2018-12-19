using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour {

	[SerializeField] private Text message;

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			message.enabled = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			message.enabled = false;
		}
	}

}
