using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform player; //Player reference
    public GameObject crystalPrefab; // projectile prefab
    public float speed; //Enemy moving speed
    public float stoppingDistance; //the ranged enemy will stop at stopping distance
    public float retreatDistance; //the ranged enemy will retreat if the distance is smaller than retreat distance
    public float awareDistance = 1.5f; //the enemy will starts to chase players

    public int health = 100;    //Enemy health
    private float damaged;  //damage animation
    private float retreatRatio = 0.6f; //retreat ratio of normal speed

    private float timeBtwShots; //the time between each peojectile
    public float startTimeBtwshots;
    public GameObject projectile; //projectile reference

    //reset the time between each peojectile
    void Start()
    {
        timeBtwShots = startTimeBtwshots;
    }

    /*Set direction of projectile
     * It can only set 4 directions right now, up,down,left and right. The function will update in the future
     * The function calculate the position of the player and the enemy, Then it returns the vector3 to represent the direction
     */
    public Vector3 SetDirection()
    {
        float xDistance = Mathf.Abs(transform.position.x - player.position.x);
        float yDistance = Mathf.Abs(transform.position.y - player.position.y);
        if (xDistance > yDistance)
        {
            if (transform.position.x < player.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 0);
                return vect;
            }
            else if (transform.position.x > player.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 180);
                return vect;
            }
        }
        else
        {
            if (transform.position.y < player.position.y)
            {
                Vector3 vect = new Vector3(0, 0, 90);
                return vect;
            }
            else if (transform.position.y > player.position.y)
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

                Instantiate(projectile, transform.position, Quaternion.Euler(SetDirection()));
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
        if (vect.magnitude <= 1.5)
        {
            if (vect.magnitude > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * 0.1f);
                /*
                if(vect.magnitude <= 2){
                    this.GetComponent<Rigidbody2D>().velocity = vect*0.5f;
                }
                */
            }
            else if (vect.magnitude < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime * 0.1f * retreatRatio);
            }
            //EnemyShooting();
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
        if (player != null)
        {
            Vector3 vect = player.position - transform.position;
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
        var go = Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        go.GetComponent<DroppedItem>().Target = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            player.GetComponent<Player1Controller>().health -= 10;
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