using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2BulletShot : MonoBehaviour {

    // Use this for initialization
    void Start() {
        var xr = Input.GetAxis("P2ShootX");
        var yr = Input.GetAxis("P2ShootY");

        this.GetComponent<Rigidbody2D>().velocity = new Vector3(xr * 3, yr * 3, 0);
        Destroy(gameObject, 1);

    }

    // Update is called once per frame
    void Update() {

    }
}
