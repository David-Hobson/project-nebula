using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Not Finished yet

public class SlowMage : HealMage {

    private GameObject target;

    // Use this for initialization
    public override void Start()
    {

        base.Start();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //health -= playerDamage;
            Destroy(other.gameObject);
            damaged = 0;
        }

        if (health <= 0)
        {
            //FindTarget(other);
            Destroy(gameObject);
        }
    }
    */
    /*
    public void FindTarget(Collider2D other)
    {
        if (other.gameObject.name == "P1Projectile")
        {
            target = GameObject.Find("Player 1");

            target.GetComponent<Player1Controller>().DecreaseSpeed(1);
            playerDamage += 10;

            if (target.GetComponent<Player1Controller>().GetDot() > 0)
            {
                target.GetComponent<Player1Controller>().IncreaseDot(-1);
            }
        }
        else if (other.gameObject.name == "P2Projectile")
        {
            target = GameObject.Find("Player 2");

            target.GetComponent<Player2Controller>().DecreaseSpeed(1);
            playerDamage += 10;

            if (target.GetComponent<Player2Controller>().GetDot() > 0)
            {
                target.GetComponent<Player2Controller>().IncreaseDot(-1);
            }
        }
    }
    */
}

