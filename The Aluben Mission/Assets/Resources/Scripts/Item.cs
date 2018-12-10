using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {


    private int owner; //who the item is on or for
    private bool isActive; //is the item currently being used
    private float duration; //how long does the item last
    public int cost; //how much does the item cost to buy
    public int tier; //what tier is the item
    public int[] displayLocation; //display location in the shop and inventory 
    public bool inShop; //is the item in the shop


    public void Purchase() { }

    /*To be Implemented
     * display item in inventory and shop
     */
    public void Display() { }

    public float GetDuration() { return duration; } //get how long an item lasts

    public bool GetActive() { return isActive; } //get if the item is currently active

    public int GetOwner() { return owner; } //get which player the item is on

    /* To be implemented:
     * activate the effects for when an item is used
     */
    public void UseItem() { isActive = true; } 

    public void PickupItem(int player) { owner = player; } //item is picked up 

    /* To be implemented:
     * Delete an item after its
     */
    public void DeleteItem() { }

}
