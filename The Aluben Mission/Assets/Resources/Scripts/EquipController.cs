using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipController : MonoBehaviour {

    private string[] guns = new string[] { "Pistol", "Cannon", "MG", "Laser" };
    public GameObject p1menu;
    public GameObject p2menu;
    public GameObject player1;
    public GameObject player2;
    public bool[] unlock = new bool[4];
    public AudioSource success;
    public AudioSource failure;
    public bool isOpen = false;

    public void Player1Equip(int index)
    {
        if (unlock[index])
        {
            player1.GetComponent<PlayerController>().EquipWeapon(index);
            success.Play();
        }
        else
            failure.Play();
    }

    public void Player2Equip(int index)
    {
        if (unlock[index])
        { 
            player2.GetComponent<PlayerController>().EquipWeapon(index);
            success.Play();
        }
        else
            failure.Play();
    }



    public void openEquip(int player) {
        this.isOpen = true;
        if (player == 1)
            p1menu.SetActive(true);
        else
            p2menu.SetActive(true);
    }

    public void closeMenu() {
        this.isOpen = false;
        p1menu.SetActive(false);
        p2menu.SetActive(false);
    }

    public bool isClosed()
    {
        return !this.isOpen;
    }

    public void updateButton(string button, int tier, bool unlocked)
    {
        if (unlocked)
            GameObject.Find(button).GetComponent<Text>().text = "Tier: " + tier;
        else
            GameObject.Find(button).GetComponent<Text>().text = "Locked";
        
    }

    // Use this for initialization
    void Start () {
        p1menu = GameObject.Find("Equip Menu");
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        int[] prices = GameObject.Find("ShopControl").GetComponent<Shop>().getPrice();
        for (int i = 0; i < 4; i++) { 
            if (prices[i] == 0)
                unlock[i] = true;
            else
                unlock[i] = false;
            updateButton("P1" + guns[i]+"Tier", PlayerPrefs.GetInt("P1" + guns[i]), unlock[i]);
            updateButton("P2" + guns[i]+"Tier", PlayerPrefs.GetInt("P2" + guns[i]), unlock[i]);
        }
        p1menu.SetActive(false);
        this.isOpen = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!this.isOpen)
            p1menu.SetActive(false);
    }
}
