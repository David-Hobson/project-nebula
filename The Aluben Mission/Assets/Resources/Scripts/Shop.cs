using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public List<Item> items;
    public List<Weapon> weapons;
    public Nebulite nebulite = new Nebulite(100);
    public Inventory inventory = new Inventory();
    
    public void AddItem(int cost, int tier, int[] display, bool inShop)
    {
        items.Add(new Item(cost, tier, display, inShop));
    }
    public void AddItem(Item item)
    {
        items.Add(item);
    }
    public void AddWeapon(int cost, int tier, int[] display, bool inShop)
    {
        weapons.Add(new Weapon(cost, tier, display, inShop));
    }
    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void UpdateShop() { }

    public bool PurchaseItem(int index) { return true; }

    public bool PurchaseWeapon(int index) { return true; }

    public void NotEnoughNebulite() { }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
