using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items;
    public List<Weapon> weapons;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item) { items.Add(item); }

    public void UseItem(int index) {
        items[index].UseItem();
        items.RemoveAt(0);
    }

    public int Display() { return 1; }

    public int ShowInUI() { return 1; }
	
    public void EquipItem(int index)
    {
        items[index].UseItem();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
