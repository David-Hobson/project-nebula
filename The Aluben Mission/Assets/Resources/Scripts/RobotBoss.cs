using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotBoss : Demon {

    private GameObject trackProjectile;
    private float stoppingDistance;
    private float retreatDistance;
    private float retreatRatio;
    private Animator anim;

    private bool iron;
    private int burst;
    private bool burstDamage;
    private float timeBtwTrackShots;
    private float startTimeBtwTrackshots;

    private float ironTime;

    private AudioSource audSource;
    public AudioClip shot;
    public AudioClip ironSound;

    public override void Start () {
        base.Start();

        stoppingDistance = 1.0f;
        retreatDistance = 0.8f;
        retreatRatio = 1.5f;
        anim = GetComponent<Animator>();
        iron = false;
        burstDamage = true;
        startTimeBtwTrackshots = 8.0f;
        timeBtwTrackShots = startTimeBtwTrackshots;
        ironTime = 6.0f;
        audSource = this.GetComponent<AudioSource>();
        shot = audSource.clip;
        trackProjectile = Resources.Load<GameObject>("Prefabs/TrackFire");
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

    public override void SetEnemyStatus()
    {
        projectile = Resources.Load<GameObject>("Prefabs/EnemyFire");
        SetStartTimeBtwShots();

        SetHealth(2000);
        SetSpeed(5.0f);
        SetAwareDistance(1.5f);
        SetEnemyDmg(10);
        SetNebuliteQuantity(10);
    }

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
        //Target().transform.GetComponent<Player1Controller>().Damage(dmg);
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
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
