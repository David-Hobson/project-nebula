using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzle : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject beam;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        beam = GameObject.Find("Beam");

    }

    // Update is called once per frame
    void Update() {
        CheckPlayersEnergized();
    }

    //Checks if players are energized and creates the beam between players
    private void CheckPlayersEnergized(){
        if(player1.GetComponent<PlayerController>().IsEnergized() && player2.GetComponent<PlayerController>().IsEnergized()){
            beam.SetActive(true);
        }else{
            beam.SetActive(false);
        }
    }
}
