using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public string name; //the name
    public bool aquired; //if the player owns the weapon
    public int cost; //the cost to purchase or upgrade the weapon
    public int tier; //the tier of the gun for its stats
    public int[] displayLocation; //the location in the shop to purchase or upgrade
    public bool inShop; //if it is in the shop or not (if fully upgraded not in shop)

    //constructor
    public Weapon(string n, int c, int t, int[] d, bool show)
    {
        name = n;
        cost = c;
        tier = t;
        displayLocation = d;
        inShop = show;
    }

    public void AquireWeapon() { aquired = true; } //on successful pickup or purchase weapon is 

    //FR 17, 18
    //Upgrade the weapon
    public void UpgradeWeapon() { 
        cost += 100; //increase the cost to upgrade weapon in the future
        tier++; //increase the 
        //change stats based of teir
    }

    public string GetName() { return name; } //get the name
    public int GetCost() { return cost; } //get the cost to upgrade
    public int GetTier() { return tier; } //get the current weapon tier (higher is better)

    //FR 52
    /* To be implemented:
     * change the equiped weapon to this one, ensure weapon is aquired
     */
    public void EquipWeapon() { }

}
