using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance; //persists between levels

    public List<Item> items; //list of items in inventory
    public List<Weapon> weapons; //list of weapons in inventory

    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
            items = new List<Item>(); ; //to be later changed to allow save/load
            weapons = new List<Weapon>(); //to be later changed to allow save/load
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Item item) { items.Add(item); } //add item to inventory when collected

    public void UseItem(int index) { //use item from inventory slot
        items[index].UseItem();
        items.RemoveAt(0);
    }

    /*To be Implemented:
     * Display all the stats and information to the player
     */
    public int Display() { return 1; }

    /*To be Implemented:
    * Display all the images in the UI
    */
    public int ShowInUI() { return 1; }
	
    // Equip weapon from the inventory
    public void EquipItem(int index) 
    {
        weapon[index].EquipWeapon();
    }

}
