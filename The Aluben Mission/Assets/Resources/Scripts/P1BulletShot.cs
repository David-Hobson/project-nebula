using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1BulletShot : MonoBehaviour {

	// Use this for initialization
	void Start() {
        //var xr = Input.GetAxis("P1ShootX");
        //var yr = Input.GetAxis("P1ShootY");

        //CalculateBulletMovement(new Vector3(xr, yr, 0));

    }

    public void CalculateBulletMovement(Vector3 vec){
        vec.Normalize();
        this.GetComponent<Rigidbody2D>().velocity = vec * 3;
        Destroy(gameObject, 1);
    }

    public string GetBulletType(){
        return "Default";
    }
	
	// Update is called once per frame
	void Update () {

    }
}
