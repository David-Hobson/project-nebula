using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject boss;

	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        boss = GameObject.Find("Boss");
	}
	
	// Update is called once per frame
	void Update () {
        DisplayPlayer1Health();
        DisplayPlayer2Health();

        if(boss != null){
            DisplayBossHealth();
        }

    }

    void DisplayPlayer1Health(){
        var player1Status = this.transform.GetChild(0);
        if (player1 != null){
            var currentHealth = player1.GetComponent<Player1Controller>().GetHealth();
            var maxHealth = player1.GetComponent<Player1Controller>().GetMaxHealth();
            player1Status.transform.GetChild(0).GetComponent<Slider>().value = currentHealth/maxHealth;
            player1Status.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        }else{
            player1Status.transform.GetChild(0).GetComponent<Slider>().value = 0;
            player1Status.transform.GetChild(1).GetComponent<Text>().text = "0/100";
        }

    }

    void DisplayPlayer2Health() {
        var player2Status = this.transform.GetChild(1);
        if (player2 != null) {
            var currentHealth = player2.GetComponent<Player2Controller>().GetHealth();
            var maxHealth = player2.GetComponent<Player2Controller>().GetMaxHealth();
            player2Status.transform.GetChild(0).GetComponent<Slider>().value = currentHealth / maxHealth;
            player2Status.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        } else {
            player2Status.transform.GetChild(0).GetComponent<Slider>().value = 0;
            player2Status.transform.GetChild(1).GetComponent<Text>().text = "0/100";
        }
    }

    void DisplayBossHealth() {
        var bossStatus = this.transform.GetChild(2);
        if (boss != null) {
            var currentHealth = boss.GetComponent<Boss>().GetHealth();
            var maxHealth = 2000.0f;
            bossStatus.transform.GetChild(0).GetComponent<Slider>().value = currentHealth / maxHealth;
            bossStatus.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        } else {
            bossStatus.transform.GetChild(0).GetComponent<Slider>().value = 0;
            bossStatus.transform.GetChild(1).GetComponent<Text>().text = "0/2000";
        }
    }
}
