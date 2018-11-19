using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform player;
    public int health = 100;

    private float damaged;
    public int droppedItem = 0;
    public bool collisionWithWalls = false;
    public bool collisionWithEnemy = false;
    public bool collisionWithPlayer = false;
    public bool collisionWithObjets = false;

	// Use this for initialization
	void Start () {
		
	}

    public Vector3 Movement(){
        Vector3 vect = player.position - transform.position;

        return vect;
    }
	
    public void enemyAnimation(){
        Vector3 vect = player.position - transform.position;
        this.GetComponent<Animator>().SetFloat("EnemySpeedX", vect.x);
        this.GetComponent<Animator>().SetFloat("EnemySpeedY", vect.y);

        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }
        
	// Update is called once per frame
    void Update () {
        if (Movement().magnitude <= 2)
        {
            this.GetComponent<Rigidbody2D>().velocity = Movement() * 0.5f;
        }
        enemyAnimation();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            health -= 25;
            Destroy(other.gameObject);
            damaged = 0;
        }
        if(other.gameObject.name == "Walls")
        {
            collisionWithWalls = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            collisionWithEnemy = true;
        }
        if (other.gameObject.name == "Player" || other.gameObject.name == "Player2")
        {
            collisionWithPlayer = true;
        }
        else
        {
            collisionWithObjets = true;
        }

        if (health <= 0){
            Destroy(gameObject);
        }
    }

    public bool EnemyAlive(){
        if (health > 0){
            return true;
        }
        else{
            return false;
        }
    }

    public bool EnemyDies()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int dropItem(){
        if(EnemyDies() == true)
        {
            droppedItem = 1;
        }
        return droppedItem;  
    }

    public bool enemyDamaged()
    {
        if(health < 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int enemyRespawns(int number)
    {
        return number;
    }

    public int enemyHealed(int number)
    {
        health = health + number;
        return health;
    }
}
