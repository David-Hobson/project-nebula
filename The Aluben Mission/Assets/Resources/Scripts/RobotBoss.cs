using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotBoss : Demon {

    private GameObject trackProjectile;//Another type of projectile
    private float stoppingDistance;//The robot boss would stop if it is closed to the target
    private float retreatDistance;//the boss would retreat back if it is much closer to the target
    private float retreatRatio;

    private Animator anim;

    private bool iron;//when boss in iron state, it is immune from any type of damage
    private int burst;// burst damage
    private bool burstDamage;
    private float timeBtwTrackShots;//interval of another type of projectile
    private float startTimeBtwTrackshots;

    private float ironTime;//duration of iron state

    //audio souce
    private AudioSource audSource;
    public AudioClip shot;
    public AudioClip ironSound;

    public override void Start () {
        base.Start();

        SetEnemyStatus(5.0f, 2000, 6.0f, 10, 10);
        SetStartTimeBtwShots(2.0f);

        stoppingDistance = 1.0f;
        retreatDistance = 0.8f;
        retreatRatio = 1.5f;

        projectile = Resources.Load<GameObject>("Prefabs/EnemyFire");
        trackProjectile = Resources.Load<GameObject>("Prefabs/TrackFire");
        anim = GetComponent<Animator>();

        iron = false;
        burstDamage = true;
        startTimeBtwTrackshots = 8.0f;
        timeBtwTrackShots = startTimeBtwTrackshots;
        ironTime = 6.0f;
        audSource = this.GetComponent<AudioSource>();
        shot = audSource.clip;
    }
	

	public override void FixedUpdate () {
        if (Target() != null)
        {
            base.FixedUpdate();
            FaceDirection();
            Vector3 vect = Target().transform.position - transform.position;
            Movement(vect);

            CheckHealth();
            TakeDamagedAnimation();
            TrackShooting(vect);
            EnemyShooting(vect);
        }
	}

    /*
     * enemy shooting method
     */ 
    public override void EnemyShooting(Vector3 vect)
    {
        if (iron == false)
        {
            if (vect.magnitude <= getAwareDistance())
            {
                if (timeBtwShots <= 0)
                {
                    audSource.PlayOneShot(shot, 1f);
                    anim.SetBool("isShooting", true);
                    Instantiate(projectile, transform.position, Quaternion.Euler(SetDirection()));
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    anim.SetBool("isShooting", false);
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }   
    }

    /*
     * Check health to start iron state
     */ 
    public void CheckHealth()
    {
        if (ironTime <= 0)
        {
            iron = false;
            anim.SetBool("isStage2", false);
            if (burstDamage == true)
            {
                DamagePlayer(burst);
                burstDamage = false;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
        else
        {
            if (health <= 400)
            {
                iron = true;
                anim.SetBool("isStage2", true);
                ironTime -= Time.deltaTime;
            }
            this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        }
    }

    public void DamagePlayer(int dmg)
    {
        Target().transform.GetComponent<Player1Controller>().Damage(dmg);
    }

    /*
     * another type of projectile
     * this projectile tracks players
     */ 
    public void TrackShooting(Vector3 vect)
    {
        if (iron == false)
        {
            if (vect.magnitude <= getAwareDistance())
            {
                if (timeBtwTrackShots <= 0)
                {
                    anim.SetBool("isShooting", false);
                    timeBtwShots = startTimeBtwShots;

                    anim.SetBool("isShooting2", true);
                    audSource.PlayOneShot(shot, 1f);
                    Instantiate(trackProjectile, transform.position + new Vector3(0, -0.1f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, -0.05f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, 0f), Quaternion.Euler(SetDirection()));
                    Instantiate(trackProjectile, transform.position + new Vector3(0, 0.05f), Quaternion.Euler(SetDirection()));

                    timeBtwTrackShots = startTimeBtwTrackshots;
                }
                else
                {
                    anim.SetBool("isShooting2", false);
                    timeBtwTrackShots -= Time.deltaTime;
                }
            }

        }
    }

    /*
     * change the direction of enemy sprite
     */ 
    public void FaceDirection()
    {
        if (transform.position.x <= Target().transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /*
     * override method of movement
     * boss's movement is different from parent movement method
     */ 
    public override void Movement(Vector3 vect)
    {
        if (iron == false)
        {
            if (vect.magnitude <= getAwareDistance())
            {
                if (vect.magnitude > stoppingDistance)
                {
                    anim.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, GetSpeed() * Time.deltaTime * 0.1f);
                }
                else if (vect.magnitude < retreatDistance)
                {
                    anim.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, -GetSpeed() * Time.deltaTime * 0.1f * retreatRatio);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    /*
     * collision check method
     */ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "P1Bullet" || other.gameObject.tag == "P2Bullet")
        {
            Destroy(other.gameObject);
            if (iron == false)
            {
                health -= 25;
                damaged = 0;
            }
            else
            {
                health -= 1;
                //burst += 25;
                audSource.PlayOneShot(ironSound, 1f);
                Instantiate(trackProjectile, transform.position, Quaternion.Euler(SetDirection()));
            }
        }


        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
            SceneManager.LoadScene(0);
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
