﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float speed; //the speed of the projectile
    private Transform player; // reference of player
    private Vector2 target; //target's position


    /*Use this for initialization
     * Set the default reference
    */
    void Start()
    {
        player = GameObject.Find("Player 1").transform;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

    }

    /* Update is called once per frame
     * Movement of projectile
     */
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Destroy(gameObject, 1);
        /*
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
        */

    }

    /*Requirement: F-10, F-11
     * Collision function between player and the projectile
     */


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            player.GetComponent<Player1Controller>().Damage(10);
            Destroy(gameObject);

        }

        if (other.gameObject.name == "Player 2") {
            player.GetComponent<Player2Controller>().Damage(10);
            Destroy(gameObject);

        }

    }
}