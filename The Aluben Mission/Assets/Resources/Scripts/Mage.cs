using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{

    private Animator mageAnim;
    private GameObject player1;
    private GameObject player2;
    private GameObject nebulite;
    //private GameObject bloodMage;
    //private GameObject slowMage;
    private GameObject healMage;
    private int playerNumber;
    public Vector2 tentacleTarget;

    public float health;
    private int collisionDMG;
    private int magicDMG;
    private float damaged;
    private float awareDistance;
    private float speed = 15.0f;

    private float timeBtwMagics;
    private float startTimeBtwMagics;
    private float timeBtwMoves;
    private float startTimeBtwMoves;
    private float timeBtwSpawn;
    private float startTimeBtwSpawn;
    bool isAttacking;
    bool isAttackFinished;
    private GameObject tentacle;

    private int playerDamage = 15;

    public float GetHealth()
    {
        return health;
    }

    // Use this for initialization
    void Start()
    {
        //playerDamage = GetComponent<HealMage>().playerDamage;
        mageAnim = GetComponent<Animator>();
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");
        tentacle = Resources.Load<GameObject>("Prefabs/Tentacle");
        //slowMage = Resources.Load<GameObject>("Prefabs/SlowMage");
        //bloodMage = Resources.Load<GameObject>("Prefabs/BloodMage");
        healMage = Resources.Load<GameObject>("Prefabs/HealMage");

        health = 2000;
        collisionDMG = 10;
        magicDMG = 20;
        awareDistance = 2.5f;

        startTimeBtwMagics = 10.0f;
        timeBtwMagics = startTimeBtwMagics;
        startTimeBtwMoves = 6.0f;
        timeBtwMoves = startTimeBtwMoves;
        startTimeBtwSpawn = 20.0f;
        timeBtwSpawn = startTimeBtwSpawn;

        isAttacking = false;
        isAttackFinished = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target() != null)
        {
            //playerDamage = GetComponent<HealMage>().playerDamage;
            Vector3 vect = Target().transform.position - transform.position;
            Movement(vect);

            TakeDamagedAnimation();
            EnemyShooting(vect);
            SpawnEnemy(vect);

        }
    }

    public void GetPlayerNumber()
    {
        playerNumber = Random.Range(1, 3);
        if (playerNumber == 1)
        {
            tentacleTarget = new Vector2(player1.transform.position.x, player1.transform.position.y - 0.1f);
        }
        else
        {
            tentacleTarget = new Vector2(player2.transform.position.x, player2.transform.position.y - 0.1f);
        }
    }

    public void SpawnEnemy(Vector3 vect)
    {
        if (vect.magnitude <= awareDistance)
        {
            if (timeBtwSpawn <= 0)
            {
                Instantiate(healMage, transform.position + new Vector3(0.5f, 0.5f), Quaternion.identity);
                Instantiate(healMage, transform.position + new Vector3(-0.5f, 0.5f), Quaternion.identity);

                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }
 
    public void EnemyShooting(Vector3 vect)
    {
        float r = 0.8f; // radius of tentacle circle

        if (vect.magnitude <= awareDistance)
        {
            if (timeBtwMagics <= 0)
            {
                GetPlayerNumber();
                for (int i = 0; i < 15; i++)
                {
                    var go = Instantiate(tentacle, tentacleTarget + new Vector2(r*Mathf.Cos(2.0f*3.14f*i/15.0f), r*Mathf.Sin(2.0f * 3.14f * i / 15.0f)), Quaternion.identity);
                    go.GetComponent<Tentacle>().target = tentacleTarget;
                    //(R * cos(2 * PI * i / n), R * sin(2 * PI * i / n))
                }
                timeBtwMagics = startTimeBtwMagics;
            }
            else
            {
                timeBtwMagics -= Time.deltaTime;
            }
        }
    }
    
    public void DamagePlayer(int dmg)
    {
        //player.
    }

    public void StartAttacking()
    {
        isAttacking = true;
    }

    public void FinishAttack()
    {
        isAttackFinished = true;
    }
    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    public void Movement(Vector3 vect)
    {
        if (vect.magnitude <= awareDistance)
        {
            if (timeBtwMoves <= 0)
            {
                mageAnim.SetBool("isMoving", true);
                if (isAttacking)
                {
                    //transform.position = Target().transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, speed * Time.deltaTime * 0.1f);
                    mageAnim.SetBool("isAttacking", true);//delete or not
                    if(isAttackFinished)
                    {
                        timeBtwMoves = startTimeBtwMoves;
                    }
                }
            }
            else
            {
                isAttacking = false;
                isAttackFinished = false;
                mageAnim.SetBool("isMoving", false);
                mageAnim.SetBool("isAttacking", false);
                timeBtwMoves -= Time.deltaTime;
            }
        }
    }

    public GameObject Target()
    {
        if (player1 == null && player2 == null)
        {
            return null;
        }
        else if (player1 == null)
        {
            return player2;
        }
        else if (player2 == null)
        {
            return player1;
        }
        else
        {
            float rangePlayer1 = Vector3.Distance(transform.position, player1.transform.position);
            float rangePlayer2 = Vector3.Distance(transform.position, player2.transform.position);
            if (rangePlayer1 < rangePlayer2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
    }
//---------------------------------
 

    public Vector2 TargetCurrentPosition()
    {
        Vector2 position = new Vector2(Target().transform.position.x, Target().transform.position.y);
        return position;
    }

    public Vector2 TargetCurrentPosition(GameObject player)
    {
        Vector2 position = new Vector2(player.transform.position.x, player.transform.position.y);
        return position;
    }


    public void DropItem()
    {
        for (int i = 0; i < 20; i++)
        {
            var go = Instantiate(nebulite, transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = Target().transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= playerDamage;
            Destroy(other.gameObject);
            damaged = 0;
        }


        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }

    }

}
