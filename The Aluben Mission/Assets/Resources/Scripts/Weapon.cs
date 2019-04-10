using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon()
    {
        aquired = false;
        tier = 0;
    }
    public bool aquired;
    public int cost;
    public int tier;
    public int[] displayLocation;
    public bool inShop;

    public Weapon(int c, int t, int[] d, bool show)
    {
        cost = c;
        tier = t;
        displayLocation = d;
        inShop = show;
    }

    public void AquireWeapon() { aquired = true; }

    public void UpgradeWeapon() {
        cost += 100;
        tier++; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
