using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorldManager : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;


    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
    }
	
	// Update is called once per frame
	void Update () {

        float vect = Vector3.Distance(this.transform.position, player1.transform.position);

        if(vect < 1.16f && player1.GetComponent<Player1Controller>().GetInteraction()) {
            Debug.Log("ENTERED!");
        }
        
	}
}
