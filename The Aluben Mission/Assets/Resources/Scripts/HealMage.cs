using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMage : MonoBehaviour {
    private GameObject HealTarget;
    protected int health = 100;
    protected float damaged;
    //public int playerDamage;
    private float timeBtwHeal;
    private float startTimeBtwHeal;

    // Use this for initialization
    public virtual void Start () {
        //playerDamage = 25;
        HealTarget = GameObject.Find("ShadowBoss");
        startTimeBtwHeal = 1.0f;
        timeBtwHeal = startTimeBtwHeal;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        Heal();
        TakeDamagedAnimation();

    }

    public void Heal()
    {
        if (timeBtwHeal <= 0)
        {
            HealTarget.GetComponent<Mage>().health += 50;
            timeBtwHeal = startTimeBtwHeal;
        }
        else
        {
            timeBtwHeal -= Time.deltaTime;
        }
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

    public void TakeDamagedAnimation()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
        damaged += 0.1f;
    }
}
