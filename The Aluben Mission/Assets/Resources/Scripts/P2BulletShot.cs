using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script may be removed when fully implemeneted Weapon mechanics
public class P2BulletShot : MonoBehaviour {

    //Initialize bullet based on right stick position of player 1
    void Start() {
        var xr = Input.GetAxis("P2RSX");
        var yr = Input.GetAxis("P2RSY");

        if (Mathf.Abs(xr) < 0.1 && Mathf.Abs(yr) < 0.1) {
            CalculateBulletMovement(new Vector3(1, 0, 0));
        } else {
            CalculateBulletMovement(new Vector3(xr, yr, 0));
        }

    }

    //REQUIREMENT: F-9
    //Calculate the bullet speed, destroy the bullet after 1 second
    public void CalculateBulletMovement(Vector3 vec) {
        vec.Normalize();
        this.GetComponent<Rigidbody2D>().velocity = vec * 3;
        Destroy(gameObject, 1);
    }

    public string GetBulletType() {
        //TODO

        //Return the type of bullet
        return "Default";
    }

    // Update is called once per frame
    void Update() {

    }
}
