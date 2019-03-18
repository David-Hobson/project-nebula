using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : NewEnemy {

    protected float timeBtwShots;
    protected float startTimeBtwShots;
    protected GameObject projectile;

    public void SetStartTimeBtwShots()
    {
        startTimeBtwShots = 2.0f;
        timeBtwShots = startTimeBtwShots;
    }

    public override void SetEnemyStatus()
    {
        projectile = Resources.Load<GameObject>("Prefabs/EnemyFire");
        SetStartTimeBtwShots();

        SetHealth(100);
        SetSpeed(5.0f);
        SetAwareDistance(1.8f);
        SetEnemyDmg(10);
        SetNebuliteQuantity(2);
    }

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

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        Vector3 vect = Target().transform.position - transform.position;
        base.FixedUpdate();
        EnemyShooting(vect);
    }
}

