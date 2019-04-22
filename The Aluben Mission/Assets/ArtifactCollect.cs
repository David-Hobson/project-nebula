using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtifactCollect : MonoBehaviour {

    public bool receivedArtifact;

    private GameObject player1;
    private GameObject player2;

    private GameObject victoryPanel;

    public GameObject fade;

    private float timing;

    // Use this for initialization
    void Start () {
        receivedArtifact = false;
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        victoryPanel = GameObject.Find("VicPanel");

        fade = GameObject.Find("Fade");

        timing = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if(receivedArtifact){
            victoryPanel.transform.GetChild(0).gameObject.SetActive(true);
            victoryPanel.transform.GetChild(1).gameObject.SetActive(true);
            victoryPanel.transform.GetChild(2).gameObject.SetActive(true);
            timing += Time.deltaTime;
        }

        if(timing >= 3.0){
            fade.GetComponent<Image>().color = new Color(0, 0, 0, (timing - 3.0f)/2);
        }

        if(timing >= 6.5){
            SceneManager.LoadScene("LoadingHome");
        }
	}

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player"){
            receivedArtifact = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
