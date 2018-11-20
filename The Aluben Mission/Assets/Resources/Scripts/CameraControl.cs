using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class CameraControl : MonoBehaviour {

    public Transform player1;
    public Transform player2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player1.position;
	}
}
