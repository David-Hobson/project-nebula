﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {
    private GameObject player1;
    private GameObject player2;

    private GameObject p1Bullet;
    private GameObject p2Bullet;

    private GameObject p1Bullet0;
    private GameObject p1Bullet1;
    private GameObject p1Bullet2;
    private GameObject p1Bullet3;

    private bool follow1;
    private bool follow2;
    private bool pickAvailable;
    // Use this for initialization
    void Start() {

        p1Bullet0 = Resources.Load<GameObject>("Prefabs/P1Projectile0");
        p1Bullet1 = Resources.Load<GameObject>("Prefabs/P1Projectile1");
        p1Bullet2 = Resources.Load<GameObject>("Prefabs/P1Projectile2");
        p1Bullet3 = Resources.Load<GameObject>("Prefabs/P1Projectile3");

        follow1 = false;
        follow2 = false;
        pickAvailable = true;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        p1Bullet = Resources.Load<GameObject>("Prefabs/P1Projectile");
        p2Bullet = Resources.Load<GameObject>("Prefabs/P2Projectile");

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), GameObject.Find("Main Camera").GetComponent<EdgeCollider2D>());

    }

    // Update is called once per frame
    void Update() {
        FollowPlayer();

    }

    public void FollowPlayer() {
        if (follow1 == true) {
            transform.position = player1.transform.position;
            p1Bullet0.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
            p1Bullet1.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
            p1Bullet2.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
            p1Bullet3.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);

        }
        if (follow2 == true) {
            transform.position = player2.transform.position;
            p2Bullet.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (pickAvailable == true) {
            if (other.gameObject.name == "Player 1") {
                follow1 = true;
            }
            if (other.gameObject.name == "Player 2") {
                follow2 = false;
            }
        }
        if (other.gameObject.tag == "Fire") {
            if (follow1 == true || follow2 == true) {
                if (follow1 == true && GameObject.FindWithTag("Fire").GetComponent<Fire>().target == player1) {
                    follow1 = false;
                    p1Bullet0.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    pickAvailable = true;
                }
                if (follow2 == true && GameObject.FindWithTag("Fire").GetComponent<Fire>().target == player2) {
                    follow2 = false;
                    p2Bullet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    pickAvailable = true;
                } else {
                    p1Bullet0.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p1Bullet3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }


    }
}