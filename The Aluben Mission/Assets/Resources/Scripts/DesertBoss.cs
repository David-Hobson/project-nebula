using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertBoss : MonoBehaviour {
    //--gameobject
    private Animator bossAnim;

    private GameObject player1;
    private GameObject player2;
    private GameObject nebulite;
    private GameObject fuel;
    //--
    private int playerNumber;
    public Vector2 FuelTarget;
    //---status
    public float health;
    private int collisionDMG;
    private float damaged;
    //-----
    private int playerDmg;


    // ---
    private float timeBtwSpawn;
    private float startTimeBtwSpawn;

    private bool hitLeftDone;
    private bool hitRightDone;
    void Start () {
        bossAnim = GetComponent<Animator>();
        fuel = Resources.Load<GameObject>("Prefabs/Fuel");
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");

        health = 20;
        collisionDMG = 10;

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.05f);

        hitLeftDone = false;
        hitRightDone = false;

        startTimeBtwSpawn = 3.0f;
        timeBtwSpawn = startTimeBtwSpawn;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Target() != null)
        {
            Vector3 vect = Target().transform.position - transform.position;
            TakeDamagedAnimation();
            SpawnFossilFuel(vect);

        }

    }

    public void GetPlayerNumber()
    {
        playerNumber = Random.Range(1, 3);
        if (playerNumber == 1)
        {
            FuelTarget = new Vector2(player1.transform.position.x, player1.transform.position.y);
        }
        else
        {
            FuelTarget = new Vector2(player2.transform.position.x, player2.transform.position.y);
        }
    }

    public void SpawnFossilFuel(Vector3 vect)
    {
            if (timeBtwSpawn <= 0)
            {
                GetPlayerNumber();
                Debug.Log("spawn");
                Instantiate(fuel, FuelTarget, Quaternion.identity);
                

                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
    }

    public void HitLeftDone()
    {
        hitLeftDone = true;
    }
    public void HitRightDone()
    {
        hitRightDone = true;
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
            health -= 25;
            Destroy(other.gameObject);
            damaged = 0;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }
        if (other.gameObject.name == "Player 1")
        {
            player1.GetComponent<Player1Controller>().Damage(collisionDMG);
        }
        if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<Player2Controller>().Damage(collisionDMG);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            player1.GetComponent<Player1Controller>().Damage(collisionDMG);
        }
        if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<Player2Controller>().Damage(collisionDMG);
        }

    }

    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    
}
