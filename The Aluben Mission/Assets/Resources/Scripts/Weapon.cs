using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int index;
    private string[] guns = new string[] { "Pistol", "Cannon", "MachineGun", "Laser" };
    public string name;
    public int fireRate;
    public float projectileSpeed;
    public int[] damage;
    public int tier;
    public GameObject bullet;
    public GameObject barrel;

    // Use this for initialization
    void Start () {
        Construct(index);
    }

    public void Construct(int i)
    {
        tier = PlayerPrefs.GetInt(guns[i]);
        switch (i)
        {
            case 0:
                name = "Pistol";
                fireRate = 4;
                damage = new int[] { 25, 38, 50, 75 };
                break;
            case 1:
                name = "Cannon";
                fireRate = 2;
                damage = new int[] { 50, 50, 75, 100, 150};
                break;
            case 2:
                name = "Machine Gun";
                fireRate = 10;
                damage = new int[] { 10, 10, 20, 30, 40 };
                break;
            case 3:
                name = "Laser";
                fireRate = 8;
                damage = new int[] { 20, 20, 30, 40, 50 };
                break;
        }

    }

    public void Upgrade()
    {
        tier++;
    }

    public int getDamage() {
        return damage[tier];
    }

    public int getFireRate() { return fireRate; }

    // Update is called once per frame
    void Update () {
		
	}
}
