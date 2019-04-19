using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzle : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject mainCamera;
    private GameObject beam;
    private GameObject barrier;

    private bool finished;
    private float camMovement;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        mainCamera = GameObject.Find("Main Camera");
        beam = GameObject.Find("Beam");

        finished = false;
        camMovement = 0;
    }

    // Update is called once per frame
    void Update() {
        CheckPlayersEnergized();

        if(finished){
            camMovement += 0.001f;
            var direction = Vector3.MoveTowards(mainCamera.transform.position, barrier.transform.position, camMovement);
            mainCamera.transform.position = new Vector3(direction.x, direction.y, -1);

            if(camMovement >= 0.2f){
                this.barrier.GetComponent<SpriteRenderer>().enabled = false;
                if(!this.barrier.GetComponent<AudioSource>().isPlaying){
                    this.barrier.GetComponent<AudioSource>().Play();
                }

            }

            if (camMovement >= 0.25) {
                finished = false;
                player1.GetComponent<PlayerController>().SetInDialogue(false);
                player2.GetComponent<PlayerController>().SetInDialogue(false);
                mainCamera.GetComponent<CameraControl>().SetInScene(false);
                Destroy(this.barrier);
            }
        }
    }

    //Checks if players are energized and creates the beam between players
    private void CheckPlayersEnergized(){
        if(player1.GetComponent<PlayerController>().IsEnergized() && player2.GetComponent<PlayerController>().IsEnergized()){
            beam.GetComponent<BoxCollider2D>().enabled = true;
            beam.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            beam.transform.position = new Vector3(beam.transform.position.x, beam.transform.position.y, 0.1f);

            if(!beam.GetComponent<AudioSource>().isPlaying){
                beam.GetComponent<AudioSource>().Play();
            }
        } else{
            beam.GetComponent<BoxCollider2D>().enabled = false;
            beam.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            beam.transform.position = new Vector3(beam.transform.position.x, beam.transform.position.y, 0);
            if (beam.GetComponent<AudioSource>().isPlaying) {
                beam.GetComponent<AudioSource>().Stop();
            }
        }
    }

    public void FinishChallenge(GameObject barrier){

        finished = true;
        this.barrier = barrier;
        player1.GetComponent<PlayerController>().SetInDialogue(true);
        player2.GetComponent<PlayerController>().SetInDialogue(true);
        player1.GetComponent<PlayerController>().SetEnergyLink(false);
        player2.GetComponent<PlayerController>().SetEnergyLink(false);
        mainCamera.GetComponent<CameraControl>().SetInScene(true);


    }

}
