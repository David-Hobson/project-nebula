using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

    public Nebulite nebulite; 
    private int[] P1Upgrades = new int[3];
    private int[] P2Upgrades = new int[3];
    private string[] upgradeNames = { "P1HP", "P1Armor", "P1Speed", "P2HP", "P2Armor", "P2Speed" };
    public Text moneyAmountText;
    private int cash;
    public AudioSource purchase;
    public AudioSource failure;

    // Use this for initialization
    void Start()
    {
        nebulite = GameObject.FindObjectsOfType<Nebulite>()[0];
        P1Upgrades = new int[] { PlayerPrefs.GetInt("P1HP"), PlayerPrefs.GetInt("P1Armor"), PlayerPrefs.GetInt("P1Speed") };
        P2Upgrades = new int[] { PlayerPrefs.GetInt("P2HP"), PlayerPrefs.GetInt("P2Armor"), PlayerPrefs.GetInt("P2Speed") };
        ButtonText("P1HP", P1Upgrades[0]);
        ButtonText("P1Armor", P1Upgrades[1]);
        ButtonText("P1Speed", P1Upgrades[2]);
        ButtonText("P2HP", P2Upgrades[0]);
        ButtonText("P2Armor", P2Upgrades[1]);
        ButtonText("P2Speed", P2Upgrades[2]);
    }

    public int Cost(int tier) 
    {
        return tier * 100;
    }

    public void ButtonText(string button, int tier)
    {
        GameObject.Find(button).GetComponentInChildren<Text>().text = "$" + Cost(tier+1);
        GameObject.Find(button).transform.GetChild(2).GetComponent<Text>().text = "Tier: " + tier;
    }

    /* TODO change Sprite
    public void ButtonImage(string button, int tier)
    {
        GameObject.Find(button).GetComponent<Image>() = 
    }
    */
    public void PurchaseP1Upgrade(int index)
    {
        int tier = P1Upgrades[index] + 1;
        int cost = Cost(tier);
        if (cost <= cash)
        {
            nebulite.RemoveNebulite(cost);
            P1Upgrades[index] += 1;
            PlayerPrefs.SetInt(upgradeNames[index], tier);
            ButtonText(upgradeNames[index], tier);
            purchase.Play();
        }
        else
            NotEnoughNebulite();
    }

    public void PurchaseP2Upgrade(int index)
    {
        int tier = P2Upgrades[index] + 1;
        int cost = Cost(tier);
        if (cost <= cash)
        {
            nebulite.RemoveNebulite(cost);
            P2Upgrades[index] += 1;
            PlayerPrefs.SetInt(upgradeNames[index+3], tier);
            ButtonText(upgradeNames[index+3], tier);
            purchase.Play();
        }
        else
            NotEnoughNebulite();
    }

    public bool PurchaseWeapon(int index) { return true; }


    public void NotEnoughNebulite()
    {
        failure.Play();
    }

	public void Return()
    {
        SceneManager.LoadScene(0);
    }


	// Update is called once per frame
	void Update () {
        cash = nebulite.GetTotal();
        moneyAmountText.text = "Nebulite: " + cash.ToString();
    }
}
