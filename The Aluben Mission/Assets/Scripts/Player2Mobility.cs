using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Mobility : MonoBehaviour {

    public float speed;

    public float shotDelay;

    public Transform bullet;

    public Animator animator;



    private void FixedUpdate() {

        var xl = Input.GetAxis("Horizontal2");
        var yl = Input.GetAxis("Vertical2");

        var xr = Input.GetAxis("P2ShootX");
        var yr = Input.GetAxis("P2ShootY");

        this.GetComponent<Rigidbody2D>().velocity = new Vector3(xl, yl, 0);

        var vect = new Vector3(xr, yr, 0);
        var angle = Vector3.SignedAngle(transform.up, vect, transform.forward) + 180;

        if (Input.GetButtonDown("P2R1")) {
            Fire();
        }

        if (xl < 0.01 && yl < 0.01) {
            animator.StopPlayback();
        }

        if (Mathf.Abs(xl) > 0 || Mathf.Abs(yl) > 0) {
            animator.SetBool("Moving", true);

            if (xl > 0) {
                animator.SetFloat("LastX", 1);
            } else if (xl < 0) {
                animator.SetFloat("LastX", -1);
            } else {
                animator.SetFloat("LastX", 0);
            }

            if (yl > 0) {
                animator.SetFloat("LastY", 1);
            } else if (yl < 0) {
                animator.SetFloat("LastY", -1);
            } else {
                animator.SetFloat("LastY", 0);
            }

        } else {
            animator.SetBool("Moving", false);
        }

        animator.SetFloat("SpeedX", xl);
        animator.SetFloat("SpeedY", yl);


    }

    private void Fire() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

}
