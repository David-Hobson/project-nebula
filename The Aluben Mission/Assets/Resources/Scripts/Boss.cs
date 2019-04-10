using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

    private Animator anim;
    public Transform player;
    public Transform player2;
    public GameObject crystalPrefab;

    public float speed; //Enemy moving speed
    public float stoppingDistance; //the ranged enemy will stop at stopping distance
    public float retreatDistance; //the ranged enemy will retreat if the distance is smaller than retreat distance
    public float awareDistance = 1.5f; //the enemy will starts to chase players
    private float retreatRatio = 1.5f;

    private int health = 2000;
    private float damaged;
    private float shootingDamage;

    private float timeBtwShots;
    private float startTimeBtwshots = 2.0f;
    public GameObject projectile1;
    public GameObject trackProjectile;

    private bool iron = false;
    private int burst;
    private bool burstDamage = true;
    public float timeBtwTrackShots;
    private float startTimeBtwTrackshots = 8.0f;

    private float ironTime = 6.0f;

    private AudioSource audSource;
    public AudioClip shot;
    public AudioClip ironSound;



    public void Start() {
        audSource = this.GetComponent<AudioSource>();
        shot = audSource.clip;
        anim = GetComponent<Animator>();
    }

    public Vector3 SetDirection() {
        if (transform.position.x < player.position.x) {
            Vector3 vect = new Vector3(0, 0, 0);
            return vect;
        } else if (transform.position.x > player.position.x) {
            Vector3 vect = new Vector3(0, 0, 180);
            return vect;
        }

        Vector3 vect1 = new Vector3(0, 0, 0);
        return vect1;
    }

    public void EnemyShooting(Vector3 vect) {
        if (iron == false) {
            if (vect.magnitude <= awareDistance) {
                if (timeBtwShots <= 0) {
                    audSource.PlayOneShot(shot, 1f);
                    anim.SetBool("isShooting", true);
                    //yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    Instantiate(projectile1, transform.position, Quaternion.Euler(SetDirection()));
                    timeBtwShots = startTimeBtwshots;
                } else {
                    anim.SetBool("isShooting", false);
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }


    public void CheckHealth() {
        if (ironTime <= 0) {
            iron = false;
            anim.SetBool("isStage2", false);
            if (burstDamage == true) {
                DamagePlayer(burst);
                burstDamage = false;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        } else {
            if (health <= 400) {
                iron = true;
                anim.SetBool("isStage2", true);
                ironTime -= Time.deltaTime;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);

        }
    }

    public void DamagePlayer(int dmg) {
        //player.GetComponent<Player1Controller>().Damage(dmg);
    }


    public void TrackShooting(Vector3 vect) {
        if (iron == false) {
            if (vect.magnitude <= awareDistance) {
                if (timeBtwTrackShots <= 0) {
                    anim.SetBool("isShooting", false);
                    timeBtwShots = startTimeBtwshots;

                    anim.SetBool("isShooting2", true);
                    audSource.PlayOneShot(shot, 1f);
                    Instantiate(trackProjectile, transform.position + new Vector3(0, -0.1f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, -0.05f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, 0f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, 0.05f), Quaternion.Euler(SetDirection()));

                    timeBtwTrackShots = startTimeBtwTrackshots;
                } else {
                    anim.SetBool("isShooting2", false);
                    timeBtwTrackShots -= Time.deltaTime;
                }
            }

        }
    }



    public void FaceDirection() {
        if (transform.position.x <= player.position.x) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void TakeDamagedAnimation() {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    public void Movement(Vector3 vect) {
        if (iron == false) {


            if (vect.magnitude <= awareDistance) {
                if (vect.magnitude > stoppingDistance) {
                    anim.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * 0.1f);
                    /*
                    if(vect.magnitude <= 2){
                        this.GetComponent<Rigidbody2D>().velocity = vect*0.5f;
                    }
                    */
                } else if (vect.magnitude < retreatDistance) {
                    anim.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime * 0.1f * retreatRatio);
                }
                  //EnemyShooting();
                  else {
                    anim.SetBool("isMoving", false);
                }
            } else {
                anim.SetBool("isMoving", false);
            }
        }
    }

    // Update is called once per frame
    public void DropItem() {
        for (int i = 0; i < 5; i++) {
            var go = Instantiate(crystalPrefab, transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = player;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") {
            Destroy(other.gameObject);
            if (iron == false) {
                health -= 25;
                damaged = 0;
            } else {
                health -= 1;
                //burst += 25;
                audSource.PlayOneShot(ironSound, 1f);
                Instantiate(trackProjectile, transform.position, Quaternion.Euler(SetDirection()));
            }
        }


        if (health <= 0) {
            Destroy(gameObject);
            DropItem();
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate() {
        if (player != null) {

            FaceDirection();
            Vector3 vect = player.position - transform.position;
            Movement(vect);

            CheckHealth();
            TakeDamagedAnimation();
            TrackShooting(vect);
            EnemyShooting(vect);

        }
    }

    public float GetHealth(){
        return health;
    }

}
