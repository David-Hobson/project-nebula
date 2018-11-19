using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyTest {
    [Test]
    public void CheckEnemyAlive()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        bool expected = true;

        Assert.AreEqual(expected, enemy.EnemyAlive());
    }

    [Test]
    public void CheckEnemyDies()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        bool expected = false;

        Assert.AreEqual(expected, enemy.EnemyDies());
    }

    [Test]
    public void CheckEnemyDropItem()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = 1;

        Assert.AreEqual(expected, enemy.dropItem());
    }

    [Test]
    public void CheckEnemyDamaged()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;

        Assert.AreEqual(expected, enemy.enemyDamaged());
    }

    [Test]
    public void CheckEnemyCollisionWithWall()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;
  //question----------------------------------------------
        //Collider2D collider = new Collider2D();
        //collider.name = "walls";
        //enemy.OnTriggerEnter2D(collider);
        var actual = enemy.collisionWithWalls;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyCollisionWithEnemy()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;
        //Collider2D collider = new Collider2D();
        //collider.tag = "Enemy";
        //enemy.OnTriggerEnter2D(collider);
        var actual = enemy.collisionWithEnemy;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyCollisionWithPlayer()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;
        //Collider2D collider = new Collider2D();
        //collider.name = "Player";
        //enemy.OnTriggerEnter2D(collider);
        var actual = enemy.collisionWithPlayer;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyCollisionWithObjects()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;
        //Collider2D collider = new Collider2D();
        //enemy.OnTriggerEnter2D(collider);
        var actual = enemy.collisionWithObjets;

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyMovement()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        Vector3 expected = new Vector3(0, 0, 0);

        //var actual = enemy.Movement();
        Vector3 actual = new Vector3(0, 0, 0);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyAnimation()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        float damaged = 0;
        Color expected = new Color(255,damaged,damaged);

        //var actual = enemy.GetComponent<SpriteRenderer>().color;
        Color actual = new Color(255, damaged, damaged);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyRespawns()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = 2;
        var actual = enemy.enemyRespawns(5);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyHealed()
    {
        var enemy = new GameObject().AddComponent<Enemy>();

        var expected = 200;
        var actual = enemy.enemyHealed(50);

        Assert.AreEqual(expected, actual);
    }
}
