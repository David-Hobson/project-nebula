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

    private float teleportTime;
    private bool teleport;

    public AudioClip teleportAura;
    public AudioClip teleportBeam;

    private float camSpeed = 0.001f;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        mainCamera = GameObject.Find("Main Camera");
        beam = GameObject.Find("Beam");

        finished = true;
        barrier = this.gameObject;

        camMovement = 0;

        teleport = true;

        player1.GetComponent<PlayerController>().SetInDialogue(true);
        player2.GetComponent<PlayerController>().SetInDialogue(true);
        player1.GetComponent<PlayerController>().SetDissapear(true);
        player2.GetComponent<PlayerController>().SetDissapear(true);

        mainCamera.GetComponent<CameraControl>().SetInScene(true);

    }

    // Update is called once per frame
    void Update() {
        CheckPlayersEnergized();
        CheckTeleport();

        if(finished){
            camMovement += camSpeed;
            var direction = Vector3.MoveTowards(mainCamera.transform.position, barrier.transform.position, camMovement);
            mainCamera.transform.position = new Vector3(direction.x, direction.y, -1);

            if(camMovement >= 0.15f){
                this.barrier.GetComponent<SpriteRenderer>().enabled = false;
                if(!this.barrier.GetComponent<AudioSource>().isPlaying){
                    this.barrier.GetComponent<AudioSource>().Play();
                }

            }

            if (camMovement >= 0.2f) {
                finished = false;
                player1.GetComponent<PlayerController>().SetInDialogue(false);
                player2.GetComponent<PlayerController>().SetInDialogue(false);
                mainCamera.GetComponent<CameraControl>().SetInScene(false);
                if(this.barrier.name != "DesertPuzzle"){
                    Destroy(this.barrier);
                }
                camMovement = 0;
            }
        }
    }

    private void CheckTeleport(){
        if(teleport){
            teleportTime += Time.deltaTime;
        }

        if(teleportTime >= 1.5f){
            player1.GetComponent<PlayerController>().SetTeleport(teleport);
            player2.GetComponent<PlayerController>().SetTeleport(teleport);
            if (!this.GetComponent<AudioSource>().isPlaying) {
                this.GetComponent<AudioSource>().PlayOneShot(teleportBeam);
            }
        }

        if(teleportTime >= 2.5f){
            teleport = false;
            this.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().PlayOneShot(teleportAura);
            player1.GetComponent<PlayerController>().SetTeleport(teleport);
            player2.GetComponent<PlayerController>().SetTeleport(teleport);
            teleportTime = 0;
            player1.GetComponent<PlayerController>().UpdatePlayer();
            player2.GetComponent<PlayerController>().UpdatePlayer();
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
