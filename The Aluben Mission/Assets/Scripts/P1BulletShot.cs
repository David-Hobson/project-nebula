using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1BulletShot : MonoBehaviour {

	// Use this for initialization
	void Start() {
        var xr = Input.GetAxis("P1ShootX");
        var yr = Input.GetAxis("P1ShootY");

        Vector3 shotVector = new Vector3(xr, yr, 0);
        shotVector.Normalize();
        this.GetComponent<Rigidbody2D>().velocity = shotVector * 3;
        Destroy(gameObject, 1);

    }
	
	// Update is called once per frame
	void Update () {

    }
}
