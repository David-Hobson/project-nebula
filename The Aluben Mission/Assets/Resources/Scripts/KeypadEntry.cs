using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadEntry : MonoBehaviour {

	public static string keyCode = "258";
	public static string enteredCode = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (enteredCode);
	}

	void onMouseUp(){
		Debug.Log ("yes");
		enteredCode += gameObject.name;
	}

	/*
	void selectButton(Input x){
		if(
	}
	*/
}
