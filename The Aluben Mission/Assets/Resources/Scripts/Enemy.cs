using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform player;
    public GameObject crystalPrefab;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
   
     
    public int health = 100;
    private float damaged;
    private float retreatRatio = 0.6f;

    private float timeBtwShots;
    public float startTimeBtwshots;
    public GameObject projectile;

    void Start()
    {
        timeBtwShots = startTimeBtwshots;
    }


    public void EnemyShooting()
    {
        if(timeBtwShots <=0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwshots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void Movement(Vector3 vect)
    {
        if (vect.magnitude <= 1.5)
        {
            if(vect.magnitude > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * 0.1f);
                /*
                if(vect.magnitude <= 2){
                    this.GetComponent<Rigidbody2D>().velocity = vect*0.5f;
                }
                */
             }
            else if(vect.magnitude < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime * 0.1f*retreatRatio);
            }
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
	// Update is called once per frame
    void FixedUpdate () {
        if(player != null){
            Vector3 vect = player.position - transform.position;
            Movement(vect);

            MovementAnimation(vect);
            TakeDamagedAnimation();
            EnemyShooting();
        }
    }

    public void DropItem()
    {
        var go = Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        go.GetComponent<DroppedItem>().Target = player;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            health -= 25;
            Destroy(other.gameObject);
            damaged = 0;
        }


        if(health <= 0){
            Destroy(gameObject);
            DropItem();
        }
    }

}
