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
    private GameObject fireMonster;
    private GameObject fire;
    private GameObject water;
    //--
    private int playerNumber;
    public Vector2 FuelTarget;
    //---status
    public float health;
    private int collisionDMG;
    private float damaged;
    //-----
    private int playerDmg;

    private Vector3 spawnPos;

    private bool waterExisted = false;

    void Start () {

        bossAnim = GetComponent<Animator>();
        fuel = Resources.Load<GameObject>("Prefabs/Fuel");
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");
        fireMonster = Resources.Load<GameObject>("Prefabs/FireMonster");
        fire = Resources.Load<GameObject>("Prefabs/Fire");
        water = Resources.Load<GameObject>("Prefabs/splash");

        health = 2000;
        collisionDMG = 10;

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.05f);

        InvokeRepeating("SpawnFuel", 3f, 2f);
    }

    public float GetHealth()
    {
        return health;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Target() != null)
        {
            Vector3 vect = Target().transform.position - transform.position;
            TakeDamagedAnimation();

            RestartFuel();
        }

    }
    public void StopFuel()
    {
            GameObject[] fuels;
            fuels = GameObject.FindGameObjectsWithTag("Fuel");
            if (fuels.Length >= 10)
            {
                CancelInvoke("SpawnFuel");
                NewFireWater();
                Debug.Log("cancel");
            }
    }

    public void RestartFuel()
    {
        if (waterExisted == true)
        {
            if (!GameObject.FindWithTag("splash"))
            {
                InvokeRepeating("SpawnFuel", 3f, 1f);
                waterExisted = false;
            }
        }
    }

    public void NewFireWater()
    {
        bossAnim.SetBool("HitRight", true);
        Instantiate(fireMonster, transform.position + new Vector3(0f, -2.0f), Quaternion.identity);

        var go = Instantiate(fire, transform.position + new Vector3(-1.0f, -1.0f), Quaternion.identity);
        //go.GetComponent<Fire>().target = Target();
        go.GetComponent<Fire>().target = player2;

        Instantiate(water, transform.position + new Vector3(1.0f, -1.0f), Quaternion.identity);

        waterExisted = true;
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

    public void SpawnFuel()
    {
        bossAnim.SetBool("HitLeft", true);

        GetPlayerNumber();
        Debug.Log("spawn" + playerNumber);
        Instantiate(fuel, FuelTarget, Quaternion.identity);

        StopFuel();
    }
    

    public void HitLeftDone()
    {
        bossAnim.SetBool("HitLeft", false);
    }
    public void HitRightDone()
    {
        bossAnim.SetBool("HitRight", false);
    }

    public GameObject Target()
    {
        if (player1 == null && player2 == null)
        {
            return null;
        }
        if (player1 == null)
        {
            return player2;
        }
        if (player2 == null)
        {
            return player1;
        }
        else
        {
            playerNumber = Random.Range(1, 3);
            if (playerNumber == 1)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
    }


    public void DropItem()
    {
        for (int i = 0; i < 20; i++)
        {
            var go = Instantiate(nebulite, transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = Target().transform;
        }
    }

    public float DistanceDamage(GameObject player)
    {
        Vector3 diff = transform.position - player.transform.position;
        float distance = diff.sqrMagnitude;

        if (distance >=0 && distance <=0.4)
        {
            return 1.0f;
        }
        if(distance > 0.4 && distance <= 0.8)
        {
            return 0.8f;
        }
        if (distance > 0.8 && distance <= 1.2)
        {
            return 0.4f;
        }
        else
        {
            return 0.1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "P1Bullet")
        {
            health -= 20 * DistanceDamage(player1);
            Destroy(other.gameObject);
            damaged = 0;
        }
        if (other.gameObject.tag == "P2Bullet")
        {
            health -= 20 * DistanceDamage(player2);
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
            player1.GetComponent<PlayerController>().Damage(collisionDMG, player1.transform.position - transform.position);
        }
        if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<PlayerController>().Damage(collisionDMG, player2.transform.position - transform.position);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            player1.GetComponent<PlayerController>().Damage(collisionDMG, player1.transform.position - transform.position);
        }
        if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<PlayerController>().Damage(collisionDMG, player2.transform.position - transform.position);
        }

    }

    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    
}
