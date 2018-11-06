using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour {

    public float speed;

    private bool paused = false;

    public float shotDelay;

    public Transform bullet;

    public Animator animator;

    public AudioSource audSource;
    public AudioClip shot;

    public Canvas pauseCanvas;



    private void Update(){

        if(!paused){
            var xl = Input.GetAxis("Horizontal");
            var yl = Input.GetAxis("Vertical");

            var xr = Input.GetAxis("P1ShootX");
            var yr = Input.GetAxis("P1ShootY");

            this.GetComponent<Rigidbody2D>().velocity = new Vector3(xl, yl, 0);

            var vect = new Vector3(xr, yr, 0);
            var angle = Vector3.SignedAngle(transform.up, vect, transform.forward) + 180;

            if (Input.GetButtonDown("P1R1")) {
                Fire();
            }



            if(xl < 0.01 && yl < 0.01){
                animator.StopPlayback();
            }

            if(Mathf.Abs(xl) > 0 || Mathf.Abs(yl) > 0) {
                animator.SetBool("Moving", true);

                if(xl > 0) {
                    animator.SetFloat("LastX", 1);
                }else if(xl < 0) {
                    animator.SetFloat("LastX", -1);
                }else{
                    animator.SetFloat("LastX", 0);
                }

                if (yl > 0) {
                    animator.SetFloat("LastY", 1);
                } else if (yl < 0) {
                    animator.SetFloat("LastY", -1);
                } else {
                    animator.SetFloat("LastY", 0);
                }

            } else{
                animator.SetBool("Moving", false);
            }

            animator.SetFloat("SpeedX", xl);
            animator.SetFloat("SpeedY", yl);
        }

        if (Input.GetButtonDown("P1Opt")) {
            Pause();
        }


    }

    private void Fire(){
        audSource.PlayOneShot(shot, 1f);
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    private void Pause() {
        if(!paused){
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            paused = true;
        }else{
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            paused = false;
        }
    }
}
