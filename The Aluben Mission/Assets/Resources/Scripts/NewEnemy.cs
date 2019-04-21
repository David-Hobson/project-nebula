using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy's base class
public class NewEnemy : MonoBehaviour {

    //Enemy's status
    private float speed;
    protected int health;
    private float awareDistance;//All enemy start to attack the players within "awareDistance"
    private int enemyDmg;
    protected float damaged; //This float affects animation about taking damage by the players
    private int nebuliteQuantity;//The number of nebulite dropped by the enemy

    private GameObject player1;
    private GameObject player2;
    private GameObject nebulite;

    /*
     * Start method
     * Initial enemy status and find important gameobject
     */
    public virtual void Start()
    {
        SetEnemyStatus(4.0f,200,1.5f,10,1);
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");
		Physics2D.IgnoreCollision (this.GetComponent<BoxCollider2D> (), GameObject.Find ("MainCamera").GetComponent<EdgeCollider2D>());
    }
    /*
     * Update method
     * Calculate the vector from the enemy to the target and call movement function
     */
    public virtual void FixedUpdate()
    {
        if (Target() != null)
        {
            Vector3 vect = Target().transform.position - transform.position;
            Movement(vect);

            TakeDamagedAnimation();
        }
    }

    //Set initial speed
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    //Return initial speed
    public float GetSpeed()
    {
        return speed;
    }
    //set initial health
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public int GetHealth()
    {
        return health;
    }

    //Set awareDistance
    public void SetAwareDistance(float awareDistance)
    {
        this.awareDistance = awareDistance;
    }
    //return awareDistance
    public float getAwareDistance()
    {
        return awareDistance;
    }
    //set enemy damage
    public void SetEnemyDmg(int enemyDmg)
    {
        this.enemyDmg = enemyDmg;
    }
    public int getEnemyDmg() { return enemyDmg; }
    //set the number of nebulite
    public void SetNebuliteQuantity(int nebuliteQuantity)
    {
        this.nebuliteQuantity = nebuliteQuantity;
    }
    /*core method to set enemt status
     * this method will call above set function
     */ 
    public virtual void SetEnemyStatus(float speed, int health, float awareDistance, int enemyDmg, int nebuliteQuantity)
    {
        SetSpeed(speed);
        SetHealth(health);
        SetAwareDistance(awareDistance);
        SetEnemyDmg(enemyDmg);
        SetNebuliteQuantity(nebuliteQuantity);
    }

    /*
     * find enemy's target, either player1 or player2
     * if both players are alive, the enemy selects the closer player 
     */
    public virtual GameObject Target()
    {

        if (player1 == null && player2 == null)
        {
            return null;
        }
        else if (player1 == null)
        {
            return player2;
        }
        else if(player2 == null)
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

    /*
     * enemy movement function
     * call moveTowards function to move the enemy
     */ 
    public virtual void Movement(Vector3 distance)
    {
        if (distance.magnitude <= awareDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, speed * Time.deltaTime * 0.1f);
            MovementAnimation(distance);
        }
    }

    /*
     * movement animation function
     * set appropriate float number to enemy animator
     */ 
    public void MovementAnimation(Vector3 vect)
    {
        this.GetComponent<Animator>().SetFloat("EnemySpeedX", vect.x);
        this.GetComponent<Animator>().SetFloat("EnemySpeedY", vect.y);
    }

    //call this method while enemy got attacked
    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    /*
     *Drop item function
     * enemy drop certain item after dead
     */ 
    public void DropItem()
    {
        for (int i =0;i<nebuliteQuantity; i++)
        {
            var go = Instantiate(nebulite, transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = Target().transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "P1Bullet" || other.gameObject.tag == "P2Bullet")
        {
            int damage = 0;
            if(other.gameObject.name.Substring(1,1) == "1")
                damage = PlayerPrefs.GetInt("P1Damage");
            else
                damage = PlayerPrefs.GetInt("P2Damage");
            health -= damage;
            Destroy(other.gameObject);
            damaged = 0;
        }
        if(other.gameObject)

        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }
			
    }

}
