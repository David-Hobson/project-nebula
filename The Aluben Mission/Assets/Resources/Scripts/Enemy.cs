using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    private GameObject nebulite;
    private float awareDistance = 1.5f;
    private float damaged;  //damage animation
    private float retreatRatio = 0.6f; //retreat ratio of normal speed
 

    public int health;
    public float speed; //Enemy moving speed
    public float stoppingDistance; //the ranged enemy will stop at stopping distance
    public float retreatDistance; //the ranged enemy will retreat if the distance is smaller than retreat distance
    public float startTimeBtwshots;

    private float timeBtwShots; //the time between each peojectile
    public GameObject projectile; //projectile reference

    //reset the time between each peojectile
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");

        timeBtwShots = startTimeBtwshots;
        Physics2D.IgnoreCollision(GameObject.Find("Main Camera").GetComponent<EdgeCollider2D>(), this.GetComponent<Collider2D>());
    }

    public GameObject Target()
    {
        float rangePlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float rangePlayer2 = Vector3.Distance(transform.position, player2.transform.position);

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

    /*Set direction of projectile
     * It can only set 4 directions right now, up,down,left and right. The function will update in the future
     * The function calculate the position of the player and the enemy, Then it returns the vector3 to represent the direction
     */
    public Vector3 SetDirection()
    {
        float xDistance = Mathf.Abs(transform.position.x - Target().transform.position.x);
        float yDistance = Mathf.Abs(transform.position.y - Target().transform.position.y);
        if (xDistance > yDistance)
        {
            if (transform.position.x < Target().transform.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 0);
                return vect;
            }
            else if (transform.position.x > Target().transform.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 180);
                return vect;
            }
        }
        else
        {
            if (transform.position.y < Target().transform.position.y)
            {
                Vector3 vect = new Vector3(0, 0, 90);
                return vect;
            }
            else if (transform.position.y > Target().transform.position.y)
            {
                Vector3 vect = new Vector3(0, 0, 270);
                return vect;
            }
        }
        Vector3 vect1 = new Vector3(0, 0, 0);
        return vect1;
    }

    /*
     * Instantiate projectile
     */
    public void EnemyShooting(Vector3 vect)
    {
        if (vect.magnitude <= awareDistance)
        {
            if (timeBtwShots <= 0)
            {
                float rotationSpeed = 2.0f;
                //Instantiate(projectile, transform.position, Quaternion.Euler(SetDirection()));
                //projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, Quaternion.LookRotation(Target().transform.position - projectile.transform.position), rotationSpeed * Time.deltaTime);
                //Vector3 relativePos = Target().transform.position - transform.position;
                Instantiate(projectile, transform.position, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target().transform.position - transform.position), rotationSpeed * Time.deltaTime));
                projectile.transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
                timeBtwShots = startTimeBtwshots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    /*Enemy movement fucntion
     * if the player is closed to the enemy, the enemy will chase the player.
     * If the distance between the enemy and the player is too closed, the enemy will retreat
     */
    public void Movement(Vector3 vect)
    {
        if (vect.magnitude <= awareDistance)
        {
            if (vect.magnitude > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, speed * Time.deltaTime * 0.1f);
            }
            else if (vect.magnitude < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, -speed * Time.deltaTime * 0.1f * retreatRatio);
            }
        }
    }

    //Update the enemy moving animation

    public void MovementAnimation(Vector3 vect)
    {
        this.GetComponent<Animator>().SetFloat("EnemySpeedX", vect.x);
        this.GetComponent<Animator>().SetFloat("EnemySpeedY", vect.y);
    }

    //Requirement: F-15
    //Update the enemy animation if taken damage
    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target() != null)
        {
            Vector3 vect = Target().transform.position - transform.position;
            Movement(vect);

            MovementAnimation(vect);
            TakeDamagedAnimation();
            EnemyShooting(vect);
        }
    }

    /* Requirement: F-15, F-16, F-24
     * Enemy will drop item after death
     * It will only drop crystal right now. The function will update in the future.
     */
    public void DropItem()
    {
        var go = Instantiate(nebulite, transform.position, Quaternion.identity);
        go.GetComponent<DroppedItem>().Target = Target().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            player1.GetComponent<Player1Controller>().Damage(10);
        }

        if(collision.gameObject.name == "Player 2"){
            player2.GetComponent<Player2Controller>().Damage(10);
        }
    }
    //Requirement: F-10, F-11, F-15
    //Collision function between the enemy and the player's projectile
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
    }

}