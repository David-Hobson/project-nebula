using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Not finished yet
public class BloodMage : HealMage {


    /*
    private int bloodDot = 0;
    public int GetDot()
    {
        return bloodDot;
    }
    public void IncreaseDot(int bloodDot)
    {
        this.bloodDot += bloodDot;
    }
    public void DecreaseSpeed(int effect)
    {
        this.speed -= effect;
    }
    public void TakeDot()
    {
        health -= bloodDot;
    }
    */


    private GameObject target;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
        base.FixedUpdate();

        this.GetComponent<SpriteRenderer>().color = new Color(255, 101, 93);
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
            //FindTarget(other);
            Destroy(gameObject);
        }
    }

    /*
    public void FindTarget(Collider2D other)
    {
        if (other.gameObject.name == "P1Projectile")
        {
            target =  GameObject.Find("Player 1");
            target.GetComponent<Player1Controller>().DecreaseSpeed(-1);
            target.GetComponent<Player1Controller>().IncreaseDot(1);

            if(playerDamage >25)
            {
                playerDamage -= 10;
            }
        }
        else if (other.gameObject.name == "P2Projectile")
        {
            target = GameObject.Find("Player 2");
            target.GetComponent<Player2Controller>().DecreaseSpeed(-1);
            target.GetComponent<Player2Controller>().IncreaseDot(1);

            if (playerDamage > 25)
            {
                playerDamage -= 10;
            }
        }
    }
    */
}
