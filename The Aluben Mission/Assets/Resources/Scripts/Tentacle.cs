using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : NewEnemy {

    private float speed;
    private GameObject player1;
    private GameObject player2;
    private int health;
    private bool rootGrowFinished;
    private Animator tentacleAnim;
    public Vector2 target;

    public override void Start() {

        speed = 0.15f;
        health = 150;

        SetEnemyStatus(speed, health, 10, 15, 1);

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        rootGrowFinished = false;
        tentacleAnim = GetComponent<Animator>();
    }


    public override void FixedUpdate() {
        if (rootGrowFinished == true) {
            tentacleAnim.SetBool("GrowFinished", true);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            TakeDamagedAnimation();
            Destroy(gameObject, 5);
        }
    }

    public void FinishGrow() {
        rootGrowFinished = true;
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "P1Bullet" || other.gameObject.tag == "P2Bullet") {
            health -= 25;
            Destroy(other.gameObject);
            damaged = 0;
        }


        if (health <= 0) {
            Destroy(gameObject);
        }
    }


}