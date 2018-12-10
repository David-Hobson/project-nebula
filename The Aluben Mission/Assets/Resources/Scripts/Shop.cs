using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public List<Item> items; //items in shop
    public List<Weapon> weapons; //weapons in shop
    public Nebulite nebulite = new Nebulite(100); //change to GameObject.FindObjectsOfType<Nebulite>()
    public Inventory inventory = new Inventory(); //change to GameObject.FindObjectsOfType<Inventory>()

    //add new item to shop 
    public void AddItem(int cost, int tier, int[] display, bool inShop)
    {
        items.Add(new Item(cost, tier, display, inShop));
    }
    //add already created item to shop
    public void AddItem(Item item)
    {
        items.Add(item);
    }
    //add new weapon to shop
    public void AddWeapon(string name, int cost, int tier, int[] display, bool inShop)
    {
        weapons.Add(new Weapon(name, cost, tier, display, inShop));
    }
    //add already created weapon to shop
    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }
    /* to be implemented:
     * update the shop after purchasing it to display the changes
     */
    public void UpdateShop() { }

    //FR 47
    /* to be implemented:
     * check if player has enough nebulite, Add the item from the shop to the player's inventory, subtract the cost
     */
    public bool PurchaseItem(int index) { return true; }

    //FR 17, 18, 47
    /* to be implemented:
     * check if player has enough nebulite, Add or upgrade the weapon to the player's inventory, subtract the cost
     */
    public bool PurchaseWeapon(int index) { return true; }

    /* to be implemented:
     * display information to the player about how they do not have enough nebulite to purchase from the shop
     */
    public void NotEnoughNebulite() { }

}
