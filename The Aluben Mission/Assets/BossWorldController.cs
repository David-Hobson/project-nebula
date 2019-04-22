using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWorldController : MonoBehaviour {

    private GameObject boss;

    private GameObject player1;
    private GameObject player2;

    public GameObject bossStatus;

    private GameObject music;

    public string bossName;

    public GameObject artifact;

    private float timing;

    private bool loaded;

    private bool ran;

	// Use this for initialization
	void Start () {
        boss = GameObject.Find(bossName);

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        player1.GetComponent<PlayerController>().UpdatePlayer();
        player2.GetComponent<PlayerController>().UpdatePlayer();

        music = GameObject.Find("Audio Source");

        bossStatus = GameObject.Find("BossStatus");

        loaded = false;
        ran = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(!loaded){
            timing += Time.deltaTime;
        }

        if(timing >= 3.0f && !loaded){
            player1.GetComponent<PlayerController>().UpdatePlayer();
            player2.GetComponent<PlayerController>().UpdatePlayer();
            timing = 0f;
            loaded = true;
        }

        if (boss == null && !ran){
            music.GetComponent<AudioSource>().Stop();
            Instantiate(artifact, this.transform);
            Destroy(bossStatus);
            ran = true;
        }
	}
}
