using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float speed; //the speed of the projectile
    private GameObject player1;
    private GameObject player2;
    private Vector2 target1; //target's position
    private Vector2 target2; //target's position

    /*Use this for initialization
     * Set the default reference
    */
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        target1 = new Vector2(player1.transform.position.x, player1.transform.position.y);
        target2 = new Vector2(player2.transform.position.x, player2.transform.position.y);
    }

    public Vector2 Target()
    {


        if (player1 == null && player2 == null)
        {
            return target1;
        }
        else if (player1 == null)
        {
            return target2;
        }
        else if (player2 == null)
        {
            return target1;
        }
        else
        {
            float rangePlayer1 = Vector3.Distance(transform.position, player1.transform.position);
            float rangePlayer2 = Vector3.Distance(transform.position, player2.transform.position);
            if (rangePlayer1 < rangePlayer2)
            {
                return target1;
            }
            else
            {
                return target2;
            }
        }
    }


    /* Update is called once per frame
     * Movement of projectile
     */
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target(), speed * Time.deltaTime);
        Destroy(gameObject, 1);

    }
}