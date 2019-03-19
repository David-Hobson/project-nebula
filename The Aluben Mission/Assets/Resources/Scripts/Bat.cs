using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : NewEnemy {

    public override void SetEnemyStatus()
    {
        SetHealth(100);
        SetSpeed(4.0f);
        SetAwareDistance(1.5f);
        SetEnemyDmg(20);
        SetNebuliteQuantity(1);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
