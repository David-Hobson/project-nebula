using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackProjectile : MonoBehaviour {

    public float speed; //the speed of the projectile
    private Transform player; // reference of player
    private Transform player2;


    private float trackTime = 2.5f;

    /*Use this for initialization
     * Set the default reference
    */
    void Start()
    {
        player = GameObject.Find("Player 1").transform;
        player2 = GameObject.Find("Player 2").transform;
    }

    public void FaceDirection()
    {
        if (transform.position.x <= player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /* Update is called once per frame
     * Movement of projectile
     */
    void FixedUpdate()
    {
        FaceDirection();
        if (trackTime >0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            trackTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            trackTime = 5.0f;
        }

    }

    /*Requirement: F-10, F-11
     * Collision function between player and the projectile
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player1Controller>().Damage(5);
            Destroy(gameObject);

        }
    }
}
