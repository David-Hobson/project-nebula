using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {
    private GameObject player1;
    private GameObject player2;

    private bool follow1;
    private bool follow2;
    // Use this for initialization
    void Start () {
        follow1 = false;
        follow2 = false;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
    }
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();

    }

    public void FollowPlayer()
    {
        if (follow1 == true)
        {
            transform.position = player1.transform.position;
            Debug.Log("follow1");
        }
        if(follow2 == true)
        {
            transform.position = player2.transform.position;
            Debug.Log("follow2");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player 1")
        {
            follow1 = true;
        }
        if (other.gameObject.name == "Player 2")
        {
            follow2 = true;
        }

    }
}
