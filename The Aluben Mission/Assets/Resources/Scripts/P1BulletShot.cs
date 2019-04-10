using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script may be removed when fully implemeneted Weapon mechanics
public class P1BulletShot : MonoBehaviour {

	//Initialize bullet based on right stick position of player 1
	void Start() {
        var xr = Input.GetAxis("P1RSX");
        var yr = Input.GetAxis("P1RSY");

        var xl = Input.GetAxis("P1LSX");
        var yl = Input.GetAxis("P1LSY");


        if (Mathf.Abs(xr) > 0.1 || Mathf.Abs(yr) > 0.1) {
            CalculateBulletMovement(new Vector3(xr, yr, 0));
        } else if (Mathf.Abs(xl) > 0.1 || Mathf.Abs(yl) > 0.1) {
            CalculateBulletMovement(new Vector3(xl, yl, 0));
        }

    }

    //REQUIREMENT: F-9
    //Calculate the bullet speed, destroy the bullet after 1 second
    public void CalculateBulletMovement(Vector3 vec){
        vec.Normalize();
        this.GetComponent<Rigidbody2D>().velocity = vec * 3;
        Destroy(gameObject, 1);
    }

    public void SetShotDirection(float xr, float yr, float xl, float yl){

    }

    public string GetBulletType(){
        //TODO

        //Return the type of bullet
        return "Default";
    }
	
	// Update is called once per frame
	void Update () {

    }
}
