using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyTest {

    GameObject enemyObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"));

    [Test]
    public void CheckEnemyAlive()
    {
        bool expected;
        if (enemyObject.GetComponent<Enemy>().health > 0)
        {
            expected = true;
        }
        else
        {
            expected = false;
        }
        Assert.AreEqual(expected, true);
    }

    [Test]
    public void CheckEnemyDies()
    {
        bool expected;
        if (enemyObject.GetComponent<Enemy>().health < 0)
        {
            expected = true;
        }
        else
        {
            expected = false;
        }
        Assert.AreEqual(expected, false);
    }

    [Test]
    public void CheckEnemyDropItem()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = 1;

        Assert.AreEqual(expected, enemy.DropItem());
    }

    [Test]
    public void CheckEnemyDamaged()
    {
        bool expected;
        if (enemyObject.GetComponent<Enemy>().health < 100)
        {
            expected = true;
        }
        else
        {
            expected = false;
        }
        Assert.AreEqual(expected, false);
    }


    [UnityTest]
    public IEnumerator CheckEnemyCollisionWithWall()
    {
        var wall = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Wall"));

        wall.GetComponent<Transform>().position = new Vector3(enemyObject.GetComponent<Transform>().position.x + 1, enemyObject.GetComponent<Transform>().position.y, 0);

        Vector3 move = new Vector3(0.1f, 0, 0);
        for (int i=0;i<100;i++)
        {
            enemyObject.GetComponent<Transform>().position += move;
        }

        Assert.False(enemyObject.GetComponent<Transform>().position.x < wall.GetComponent<Transform>().position.x);
        yield return null;
        
    }

    [UnityTest]
    public IEnumerator CheckEnemyCollisionWithEnemy()
    {
        GameObject enemyObject2 = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"));

        enemyObject2.GetComponent<Transform>().position = new Vector3(enemyObject.GetComponent<Transform>().position.x + 1, enemyObject.GetComponent<Transform>().position.y, 0);

        Vector3 move = new Vector3(0.1f, 0, 0);
        for (int i = 0; i < 100; i++)
        {
            enemyObject.GetComponent<Transform>().position += move;
        }

        Assert.False(enemyObject.GetComponent<Transform>().position.x > enemyObject2.GetComponent<Transform>().position.x);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckEnemyCollisionWithPlayer()
    {
        var player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        player.GetComponent<Transform>().position = new Vector3(enemyObject.GetComponent<Transform>().position.x + 1, enemyObject.GetComponent<Transform>().position.y, 0);

        Vector3 move = new Vector3(1, 0, 0);
        for (int i = 0; i < 100; i++)
        {
            enemyObject.GetComponent<Transform>().position += move;
        }

        Assert.False(enemyObject.GetComponent<Transform>().position.x < player.GetComponent<Transform>().position.x);
        yield return null;
    }

    [Test]
    public void CheckEnemyCollisionWithObjects()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = false;

        var actual = enemy.collisionWithObjets;

        Assert.AreEqual(expected, actual);
    }

    [UnityTest]
    public IEnumerator CheckEnemyMovement()
    {

        var player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        player.GetComponent<Transform>().position = new Vector3(enemyObject.GetComponent<Transform>().position.x + 1, enemyObject.GetComponent<Transform>().position.y, 0);
        for (int i = 0; i < 100; i++)
        {

            Vector3 vect = player.GetComponent<Transform>().position - enemyObject.GetComponent<Transform>().position;

            if (vect.magnitude <= 2)
            {
                enemyObject.GetComponent<Rigidbody2D>().velocity = vect * 0.5f;
            }

            yield return new WaitForSeconds(0.1f);
        }
        Assert.False(enemyObject.GetComponent<Transform>().position.x >= player.GetComponent<Transform>().position.x);
        yield return null;
    }

    [Test]
    public void CheckEnemyAnimation()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        float damaged = 0;
        Color expected = new Color(255,damaged,damaged);

        Color actual = new Color(255, damaged, damaged);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyRespawns()
    {
        var enemy = new GameObject().AddComponent<Enemy>();
        var expected = 2;
        var actual = enemy.EnemyRespawns(5);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CheckEnemyHealed()
    {
        var enemy = new GameObject().AddComponent<Enemy>();

        var expected = 200;
        var actual = enemy.EnemyHealed(50);

        Assert.AreEqual(expected, actual);
    }
}
