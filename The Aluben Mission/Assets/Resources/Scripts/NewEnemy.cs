using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour {

    private float speed;
    protected int health;
    private float awareDistance;
    private int enemyDmg;
    protected float damaged;
    private int nebuliteQuantity;

    private GameObject player1;
    private GameObject player2;
    private GameObject nebulite;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }


    public void SetAwareDistance(float awareDistance)
    {
        this.awareDistance = awareDistance;
    }

    public float getAwareDistance()
    {
        return awareDistance;
    }

    public void SetEnemyDmg(int enemyDmg)
    {
        this.enemyDmg = enemyDmg;
    }

    public void SetNebuliteQuantity(int nebuliteQuantity)
    {
        this.nebuliteQuantity = nebuliteQuantity;
    }

    public virtual void SetEnemyStatus()
    {
        SetSpeed(7.0f);
        SetHealth(200);
        SetAwareDistance(1.5f);
        SetEnemyDmg(10);
        SetNebuliteQuantity(1);
    }

    public virtual void Start()
    {
        SetEnemyStatus();
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        nebulite = Resources.Load<GameObject>("Prefabs/Crystal");
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

    public virtual void Movement(Vector3 distance)
    {
        if (distance.magnitude <= awareDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, speed * Time.deltaTime * 0.1f);
            MovementAnimation(distance);
        }
    }

    public void MovementAnimation(Vector3 vect)
    {
        this.GetComponent<Animator>().SetFloat("EnemySpeedX", vect.x);
        this.GetComponent<Animator>().SetFloat("EnemySpeedY", vect.y);
    }

    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }

    public void DropItem()
    {
        for (int i =0;i<nebuliteQuantity; i++)
        {
            var go = Instantiate(nebulite, transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = Target().transform;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            player1.GetComponent<Player1Controller>().Damage(enemyDmg);
        }

        if (collision.gameObject.name == "Player 2")
        {
            player2.GetComponent<Player2Controller>().Damage(enemyDmg);
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
    }

    public virtual void FixedUpdate()
    {
        if (Target() != null)
        {
            Vector3 vect = Target().transform.position - transform.position;
            Movement(vect);

            //MovementAnimation(vect);
            TakeDamagedAnimation();
        }
    }
}
