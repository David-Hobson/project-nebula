using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWorldController : MonoBehaviour {

    private GameObject boss;

    private GameObject player1;
    private GameObject player2;

    public string bossName;

    private float timing;

    private bool loaded;

	// Use this for initialization
	void Start () {
        boss = GameObject.Find(bossName);

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        player1.GetComponent<PlayerController>().UpdatePlayer();
        player2.GetComponent<PlayerController>().UpdatePlayer();

        loaded = false;

    }
	
	// Update is called once per frame
	void Update () {
        if(!loaded){
            timing += Time.deltaTime;
        }

        if(timing >= 3.0f && loaded){
            player1.GetComponent<PlayerController>().UpdatePlayer();
            player2.GetComponent<PlayerController>().UpdatePlayer();
            timing = 0f;
            loaded = true;
        }

        if(boss == null){
            timing += Time.deltaTime;
            if(timing >= 5.0f){
                SceneManager.LoadScene("HomeWorldShopWorking");
            }
        }
	}
}
