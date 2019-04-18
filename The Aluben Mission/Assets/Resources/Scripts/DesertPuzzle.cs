using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzle : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject mainCamera;
    private GameObject beam;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        mainCamera = GameObject.Find("Main Camera");
        beam = GameObject.Find("Beam");

    }

    // Update is called once per frame
    void Update() {
        CheckPlayersEnergized();
    }

    //Checks if players are energized and creates the beam between players
    private void CheckPlayersEnergized(){
        if(player1.GetComponent<PlayerController>().IsEnergized() && player2.GetComponent<PlayerController>().IsEnergized()){
            beam.GetComponent<BoxCollider2D>().enabled = true;
            beam.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            beam.transform.position = new Vector3(beam.transform.position.x, beam.transform.position.y, 0.1f);
        } else{
            beam.GetComponent<BoxCollider2D>().enabled = false;
            beam.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            beam.transform.position = new Vector3(beam.transform.position.x, beam.transform.position.y, 0);
        }
    }

    public void FinishChallenge(GameObject barrier){

        mainCamera.GetComponent<CameraControl>().SetInScene(true);

        float movement = 0;

        while (movement <= 1.0f){
            movement += 0.01f;
            var direction = Vector3.MoveTowards(mainCamera.transform.position, barrier.transform.position, movement);
            mainCamera.transform.position = new Vector3(direction.x, direction.y, -1);
        }


        //mainCamera.transform.position += new Vector3(0, 0, -1);

        //mainCamera.GetComponent<CameraControl>().SetInScene(false);

    }
}
