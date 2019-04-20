using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {
    private GameObject player1;
    private GameObject player2;

    private GameObject p1Bullet;
    private GameObject p2Bullet;

    private bool follow1;
    private bool follow2;
    private bool pickAvailable;
    // Use this for initialization
    void Start () {
        follow1 = false;
        follow2 = false;
        pickAvailable = true;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        p1Bullet = Resources.Load<GameObject>("Prefabs/P1Projectile");
        p2Bullet = Resources.Load<GameObject>("Prefabs/P2Projectile");
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
            p1Bullet.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
        }
        if(follow2 == true)
        {
            transform.position = player2.transform.position;
            p2Bullet.GetComponent<SpriteRenderer>().color = new Color(0.3443f, 0.9035f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickAvailable == true)
        {
            if (other.gameObject.name == "Player 1")
            {
                follow1 = true;
                pickAvailable = false;
            }
            if (other.gameObject.name == "Player 2")
            {
                follow2 = true;
                pickAvailable = false;
            }
        }
        if (other.gameObject.tag == "Fire")
        {
            if (follow1 == true || follow2 == true)
            {
                if (follow1 == true && GameObject.FindWithTag("Fire").GetComponent<Fire>().target == player1)
                {
                    follow1 = false;
                    p1Bullet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    pickAvailable = true;
                }
                if (follow2 == true && GameObject.FindWithTag("Fire").GetComponent<Fire>().target == player2)
                {
                    follow2 = false;
                    p2Bullet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    pickAvailable = true;
                }
                else
                {
                    p1Bullet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    p2Bullet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
        

    }
}
