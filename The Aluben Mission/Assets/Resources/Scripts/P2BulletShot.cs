using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script may be removed when fully implemeneted Weapon mechanics
public class P2BulletShot : MonoBehaviour {

    private float[] speed = { 3f, 2.5f, 4f, 12f };
    private int damage;
    public int type;

    //Initialize bullet based on right stick position of player 1
    void Start() {
        var xr = Input.GetAxis("P2RSX");
        var yr = Input.GetAxis("P2RSY");

        var xl = Input.GetAxis("P2LSX");
        var yl = Input.GetAxis("P2LSY");

        damage = PlayerPrefs.GetInt("P2Damage");

        if (Mathf.Abs(xr) > 0.1 || Mathf.Abs(yr) > 0.1) { 
            CalculateBulletMovement(new Vector3(xr, yr, 0));
        } else if (Mathf.Abs(xl) > 0.1 || Mathf.Abs(yl) > 0.1) { 
            CalculateBulletMovement(new Vector3(xl, yl, 0));
        }

    }

    //REQUIREMENT: F-9
    //Calculate the bullet speed, destroy the bullet after 1 second
    public void CalculateBulletMovement(Vector3 vec) {
        vec.Normalize();
        this.GetComponent<Rigidbody2D>().velocity = vec * speed[type];
        if (type != 3)
            Destroy(gameObject, 1);
        else
            Destroy(gameObject, 0.5f);
    }

    public string GetBulletType() {
        //TODO

        //Return the type of bullet
        return "Default";
    }

    public int getDamage() { return damage; }

    // Update is called once per frame
    void Update() {

    }
}
