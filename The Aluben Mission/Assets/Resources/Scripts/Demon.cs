using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : NewEnemy {

    protected float timeBtwShots;//interval between shots from enemy demon
    protected float startTimeBtwShots;
    protected GameObject projectile;// demon's projectile gameobject

    /*
     * call parent start method and initial shots interval
     */ 
    public override void Start()
    {
        base.Start();

        SetEnemyStatus(5.0f, 100, 1.8f, 10, 2);
        projectile = Resources.Load<GameObject>("Prefabs/EnemyFire");
        SetStartTimeBtwShots(2.0f);

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), GameObject.Find("Main Camera").GetComponent<EdgeCollider2D>());

    }

    //call parent update method and shooting method
    public override void FixedUpdate()
    {
        Vector3 vect = Target().transform.position - transform.position;
        base.FixedUpdate();
        EnemyShooting(vect);
    }
    //set shots interval
    public void SetStartTimeBtwShots(float period)
    {
        startTimeBtwShots = period;
        timeBtwShots = startTimeBtwShots;
    }

    /*
     * set projectile direction method
     * the direction of projectile changes if the positions of players changes
     */ 
    public Vector3 SetDirection()
    {
        float xDistance = Mathf.Abs(transform.position.x - Target().transform.position.x);
        float yDistance = Mathf.Abs(transform.position.y - Target().transform.position.y);
        if (xDistance > yDistance)
        {
            if (transform.position.x < Target().transform.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 0);
                return vect;
            }
            else if (transform.position.x > Target().transform.position.x)
            {
                Vector3 vect = new Vector3(0, 0, 180);
                return vect;
            }
        }
        else
        {
            if (transform.position.y < Target().transform.position.y)
            {
                Vector3 vect = new Vector3(0, 0, 90);
                return vect;
            }
            else if (transform.position.y > Target().transform.position.y)
            {
                Vector3 vect = new Vector3(0, 0, 270);
                return vect;
            }
        }
        Vector3 vect1 = new Vector3(0, 0, 0);
        return vect1;
    }

    /*
     * enemy shooting method
     * instantiate projectile gameobject
     */ 
    public virtual void EnemyShooting(Vector3 vect)
    {
        if (vect.magnitude <= getAwareDistance())
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.Euler(SetDirection()));
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

}

