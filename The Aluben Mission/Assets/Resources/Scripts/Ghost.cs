﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : NewEnemy {


/*
 * ghost class
 * set enemy status by calling parent method
 * call base start and update method
 */
    public override void Start()
    {
        base.Start();
        SetEnemyStatus(4.0f, 100, 1.5f, 20, 1);

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), GameObject.Find("Main Camera").GetComponent<EdgeCollider2D>());
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
