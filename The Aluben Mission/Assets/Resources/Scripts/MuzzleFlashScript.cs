using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
