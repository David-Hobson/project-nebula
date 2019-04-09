using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzle : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("P1X")){
            player1.GetComponent<Player1Controller>().ToggleEnergyLink();
        }

        if(Input.GetButtonUp("P1X")){
            player1.GetComponent<Player1Controller>().ToggleEnergyLink();
        }
    }



}
