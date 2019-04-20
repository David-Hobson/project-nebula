using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private int playerNumber;
    public GameObject target;
    private float speed = 0.4f;

    private bool touched1;
    private bool touched2;

    private GameObject player1;
    private GameObject player2;
    void Start () {
        touched1 = false;
        touched1 = false;


        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        InvokeRepeating("FireDamage", 1f, 1f);
    }
	
	void FixedUpdate () {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void FireDamage()
    {
        if (touched1 == true)
        {
            player1.GetComponent<Player1Controller>().Damage(2);
        }
        if (touched2 == true)
        {
            player2.GetComponent<Player2Controller>().Damage(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Player 1")
        {
            touched1 = true;
        }
        if (other.gameObject.name == "Player 2")
        {
            touched2 = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            touched1 = false;
        }
        if (other.gameObject.name == "Player 2")
        {
            touched2 = false;
        }
    }

}
