using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {

    private float speed;
    private GameObject player1;
    private GameObject player2;
    private int health;
    private float damaged;
    private bool rootGrowFinished;
    private Animator tentacleAnim;
    public Vector2 target;

    void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        speed = 0.15f;
        health = 150;
        rootGrowFinished = false;
        tentacleAnim = GetComponent<Animator>();
    }

	
	void FixedUpdate () {
        if (rootGrowFinished == true)
        {
            tentacleAnim.SetBool("GrowFinished", true);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            TakeDamagedAnimation();
            Destroy(gameObject, 5);
        }
    }

    public void FinishGrow()
    {
        rootGrowFinished = true;
    }

    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
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
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            player1.GetComponent<Player1Controller>().Damage(5);
        }

        if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<Player2Controller>().Damage(5);
        }

    }
    

}
