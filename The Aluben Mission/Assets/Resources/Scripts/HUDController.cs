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
        boss = GameObject.Find("ShadowBoss");
	}
	
	// Update is called once per frame
	void Update () {
        DisplayPlayerHealth(player1, 0);
        DisplayPlayerHealth(player2, 1);

        if(boss != null){
            DisplayBossHealth();
        }

    }

    //Updates the players current health on the HUD based on player health parameters
    //Displayed to the user for feedback of the player status
    void DisplayPlayerHealth(GameObject player, int num){
        var playerStatus = this.transform.GetChild(num);
        if (player != null){
            var currentHealth = player.GetComponent<PlayerController>().GetHealth();
            var maxHealth = player.GetComponent<PlayerController>().GetMaxHealth();
            playerStatus.transform.GetChild(0).GetComponent<Slider>().value = currentHealth/maxHealth;
            playerStatus.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        }else{
            playerStatus.transform.GetChild(0).GetComponent<Slider>().value = 0;
            playerStatus.transform.GetChild(1).GetComponent<Text>().text = "0/100";
        }

    }

    //Updates the boss' current health on the HUD based on boss health parameters
    //Displayed to the user for feedback of the boss' health status
    void DisplayBossHealth() {
        var bossStatus = this.transform.GetChild(2);
        if (boss != null) {
            var currentHealth = boss.GetComponent<Mage>().GetHealth();
            var maxHealth = 2000.0f;
            bossStatus.transform.GetChild(0).GetComponent<Slider>().value = currentHealth / maxHealth;
            bossStatus.transform.GetChild(1).GetComponent<Text>().text = currentHealth + "/" + maxHealth;
        } else {
            bossStatus.transform.GetChild(0).GetComponent<Slider>().value = 0;
            bossStatus.transform.GetChild(1).GetComponent<Text>().text = "0/2000";
        }
    }
}
