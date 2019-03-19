using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackProjectile : MonoBehaviour {

    public float speed; //the speed of the projectile
    private GameObject player1; // reference of player
    private GameObject player2;


    private float trackTime = 2.5f;

    /*Use this for initialization
     * Set the default reference
    */
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
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
        else if (player2 == null)
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

    public void FaceDirection()
    {
        if (transform.position.x <= Target().transform.position.x)
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
            transform.position = Vector2.MoveTowards(transform.position, Target().transform.position, speed * Time.deltaTime);
            trackTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            trackTime = 5.0f;
        }

    }

}
