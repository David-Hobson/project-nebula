using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour {

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

    public GameObject HUD;

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
        CheckTeleport();
    }

    private void CheckTeleport() {
        if (teleport) {
            teleportTime += Time.deltaTime;
        }

        if (teleportTime >= 1.5f) {
            player1.GetComponent<PlayerController>().SetTeleport(teleport);
            player2.GetComponent<PlayerController>().SetTeleport(teleport);
            if (!this.GetComponent<AudioSource>().isPlaying) {
                this.GetComponent<AudioSource>().PlayOneShot(teleportBeam);
            }
        }

        if (teleportTime >= 2.5f) {
            teleport = false;
            this.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().PlayOneShot(teleportAura);
            player1.GetComponent<PlayerController>().SetTeleport(teleport);
            player2.GetComponent<PlayerController>().SetTeleport(teleport);
            teleportTime = 0;
            player1.GetComponent<PlayerController>().UpdatePlayer();
            player2.GetComponent<PlayerController>().UpdatePlayer();
            mainCamera.GetComponent<CameraControl>().SetInScene(false);
            //HUD.GetComponent<Canvas>().enabled = true;
        }
    }

}
