using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour {

    public Nebulite nebulite;
    public bool isOpen = false;

    private string[] guns = new string[] { "Pistol", "Cannon", "MG", "Laser" };
    public GameObject player1;
    public GameObject player2;
    public bool[] unlock = new bool[4];
    public bool[] OpenMenu = new bool[4];
    int currentPlayer;
    private int[] P1Upgrades;
    private int[] P2Upgrades;
    private int[] playerGuns;
    private string[] upgradeNames = { "P1HP", "P1Speed", "P2HP", "P2Speed", "Pistol", "Cannon", "MG", "Laser" };
    int[] price = { 0, 500, 400, 600 };
    public GameObject p1shop;
    public GameObject p2shop;
    public GameObject p1equip;
    public GameObject p2equip;
    public Text p1moneyAmountText;
    public Text p2moneyAmountText;
    private int cash;
    public AudioSource success;
    public AudioSource failure;

    public string P1Circle;
    public string P2Circle;
    public string P1Triangle;
    public string P2Triangle;

    // Use this for initialization
    void Start()
    {
        P1Upgrades = new int[] { PlayerPrefs.GetInt("P1HP"), PlayerPrefs.GetInt("P1Speed") };
        P2Upgrades = new int[] { PlayerPrefs.GetInt("P2HP"), PlayerPrefs.GetInt("P2Speed") };
        playerGuns = new int[] { PlayerPrefs.GetInt("Pistol"), PlayerPrefs.GetInt("Cannon"), PlayerPrefs.GetInt("MG"), PlayerPrefs.GetInt("Laser") };
        if (playerGuns[0] == 0)
            playerGuns[0] = 1;
        ButtonText("P1HP", P1Upgrades[0]);
        ButtonText("P1Speed", P1Upgrades[1]);
        ButtonText("P2HP", P2Upgrades[0]);
        ButtonText("P2Speed", P2Upgrades[1]);
        for(int i = 0; i < 4; i++)
        {

            if(playerGuns[i] != 0) {
                price[i] = 0;
                ButtonText("P1" + upgradeNames[i+4] + "Buy", playerGuns[i]);
                ButtonText("P2" + upgradeNames[i + 4] + "Buy", playerGuns[i]);
                
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (price[i] == 0)
                unlock[i] = true;
            else
                unlock[i] = false;
        }
        toggleMenu(p1shop, false);
        toggleMenu(p2shop, false);
        toggleMenu(p1equip, false);
        toggleMenu(p2equip, false);
        this.isOpen = false;
    }

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

    public void updateButton(string button, int tier, bool unlocked)
    {
        button = button + "Equip";
        if (unlocked)
        {
            if (button.Equals("P1PistolEquip") || button.Equals("P2PistolEquip"))
                tier += 1;
            GameObject.Find(button).transform.GetChild(2).GetComponent<Text>().text = "Tier: " + (tier -1);
            GameObject.Find(button).transform.GetChild(0).GetComponent<Image>().color = Color.white;
            GameObject.Find(button).GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find(button).transform.GetChild(2).GetComponent<Text>().text = "Locked";
            GameObject.Find(button).transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            GameObject.Find(button).GetComponent<Button>().interactable = false;
        }
            

    }

    public int Cost(int tier) 
    {
        return tier * 100;
    }

    public void ButtonText(string button, int tier)
    {
        if (tier < 4)
        {
            GameObject.Find(button).transform.GetChild(3).GetComponent<Text>().text = "$" + Cost(tier + 1);
            GameObject.Find(button).transform.GetChild(2).GetComponent<Text>().text = "Tier: " + tier;
        }
        else
        {
            GameObject.Find(button).transform.GetChild(3).GetComponent<Text>().text = "Maximum";
            GameObject.Find(button).transform.GetChild(2).GetComponent<Text>().text = "Tier: " + tier;
        }
    }

    public void ButtonImage(string button, int player, int tier, string type)
    {
        GameObject.Find(button).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/P" + player + type + tier);
    }
    
    public void toggleMenu(GameObject obj, bool flip)
    {
        obj.GetComponent<Canvas>().enabled = flip;
        obj.GetComponent<SelectOnInput>().enabled = flip;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).GetComponent<Button>() != null)
            {
                obj.transform.GetChild(i).GetComponent<Button>().enabled = flip;
            }
            else if(obj.transform.GetChild(i).GetComponent<HorizontalLayoutGroup>() != null)
            {
                for (int j = 0; j < obj.transform.GetChild(i).childCount; j++)
                {
                    obj.transform.GetChild(i).GetChild(j).GetComponent<Button>().enabled = flip;
                }
            }
            //if (flip && i == 0) {
                //obj.transform.GetChild(0).GetComponent<Button>().OnSelect(null);
                //obj.transform.GetChild(0).GetComponent<Button>().IsHighlighted();
            //}
        }

    }

    public void PurchaseGun(int index)
    {
        int tier = playerGuns[index] +1;
        int cost = 0;
        if (price[index] != 0) { 
            cost = price[index];
        }
        else
        {
            cost = Cost(tier);
        }
            
        if (cost <= cash && tier < 5)
        {
            price[index] = 0;
            if (!unlock[index])
                unlock[index] = true;

            nebulite.RemoveNebulite(cost);
            playerGuns[index] = tier;
            PlayerPrefs.SetInt(upgradeNames[index + 4], tier);
            player1.GetComponent<PlayerController>().UpgradeWeapon(index);
            player2.GetComponent<PlayerController>().UpgradeWeapon(index);
            if (currentPlayer == 1)
                ButtonText("P1"+upgradeNames[index + 4]+"Buy", tier);
            else
                ButtonText("P2" + upgradeNames[index + 4] + "Buy", tier);
            success.Play();
            player1.GetComponent<PlayerController>().EquipWeapon(index);
            player2.GetComponent<PlayerController>().EquipWeapon(index);
        }
        else
            NotEnoughNebulite();
    }
    public void PurchaseP1Upgrade(int index)
    {
        int tier = P1Upgrades[index] + 1;
        int cost = Cost(tier);
        if (cost <= cash && tier <5)
        {
            nebulite.RemoveNebulite(cost);
            P1Upgrades[index] += 1;
            PlayerPrefs.SetInt(upgradeNames[index], tier);
            ButtonText(upgradeNames[index], tier);
            success.Play();
            GameObject.Find("Player 1").GetComponent<PlayerController>().Upgrade(index+1);
        }
        else
            NotEnoughNebulite();
    }

    public void PurchaseP2Upgrade(int index)
    {
        int tier = P2Upgrades[index] + 1;
        int cost = Cost(tier);
        if (cost <= cash && tier <5)
        {
            nebulite.RemoveNebulite(cost);
            P2Upgrades[index] += 1;
            PlayerPrefs.SetInt(upgradeNames[index+2], tier);
            ButtonText(upgradeNames[index+2], tier);
            success.Play();
            GameObject.Find("Player 2").GetComponent<PlayerController>().Upgrade(index+1);
        }
        else
            NotEnoughNebulite();
    }

    public void NotEnoughNebulite()
    {
        failure.Play();
    }

    public void openShop(int player)
    {
        nebulite.UpdateNebulite();
        EventSystem.current.SetSelectedGameObject(null);
        currentPlayer = player;
        this.isOpen = true;
        if (player == 1)
        {
            OpenMenu[0] = true;
            OpenMenu[2] = false;
            toggleMenu(p1equip, false);
            toggleMenu(p1shop, true);
        }
        else {
            OpenMenu[1] = true;
            OpenMenu[3] = false;
            toggleMenu(p2equip, false);
            toggleMenu(p2shop, true);
        }
        success.Play();
    }

    public void openEquip()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (currentPlayer == 1) {
            OpenMenu[2] = true;
            OpenMenu[0] = false;
            toggleMenu(p1equip, true);
            toggleMenu(p1shop, false);
            for (int i = 0; i < 4; i++)
                updateButton("P1" + guns[i], playerGuns[i], unlock[i]);
        }
        else {
            OpenMenu[3] = true;
            OpenMenu[1] = false;
            toggleMenu(p2equip, true);
            toggleMenu(p2shop, false);
            for (int i = 0; i < 4; i++)
                updateButton("P2" + guns[i], playerGuns[i], unlock[i]);
        }
        success.Play();
    }

    public bool isClosed()
    {
        return !this.isOpen;
    }

	public void Return()
    {
        currentPlayer = 0;
        this.isOpen = false;
        toggleMenu(p1shop, false);
        toggleMenu(p2shop, false);
        toggleMenu(p1equip, false);
        toggleMenu(p2equip, false);
    }

    public int[] getPrice() { return price; }

	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetButtonDown(P1Circle) && currentPlayer == 1)
        {
            Return();
        }
        if (Input.GetButtonDown(P2Circle) && currentPlayer == 2)
        {
            Return();
        }
        if (Input.GetButtonDown(P1Triangle) && currentPlayer == 1)
        {
            if (openMenu[0])
                openEquip();
            else
                openShop[currentPlayer];
        }
        if (Input.GetButtonDown(P1Triangle) && currentPlayer == 1)
        {
            if (openMenu[1])
                openEquip();
            else
                openShop[currentPlayer];
        }
        */

        cash = nebulite.GetTotal();
        p1moneyAmountText.text = "Nebulite: " + cash.ToString();
        p2moneyAmountText.text = "Nebulite: " + cash.ToString();
    }
}
