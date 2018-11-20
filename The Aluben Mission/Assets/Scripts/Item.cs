using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private int owner;
    private bool isActive;
    private float duration;
    public int cost;
    public int tier;
    public int[] displayLocation;
    public bool inShop;

    public Item(int c, int t, int[] d, bool show)
    {
        cost = c;
        tier = t;
        displayLocation = d;
        inShop = show;
    }

    public Item() { }

    public void Purchase() { }

    public void Display() { }

    public float GetDuration() { return duration; }

    public bool GetActive() { return isActive; }

    public int GetOwner() { return owner; }

    public void UseItem() { isActive = true; }

    public void PickupItem(int player) { owner = player; }

    public void DeleteItem() { }

    private void OnTriggerEnter2D(Collider2D other)
    {


    }

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
