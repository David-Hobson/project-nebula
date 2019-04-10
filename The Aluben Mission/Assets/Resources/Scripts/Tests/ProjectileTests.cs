using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ProjectileTests {

    public GameObject projectile;
    public GameObject player;

    [SetUp]
    public void SetUp() {
        projectile = Object.Instantiate(Resources.Load<GameObject>("Prefabs/bulleta"));
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/player"));
    }

    [TearDown]
    public void TearDown(){
        Object.Destroy(projectile);
        Object.Destroy(player);

    }


    [UnityTest]
    public IEnumerator CheckProjectileCollisionWithWall() {
        
        Assert.IsNotNull(GameObject.FindWithTag("Bullet"));

        GameObject wall = Resources.Load<GameObject>("Prefabs/Wall");
        wall.GetComponent<Transform>().position = projectile.GetComponent<Transform>().position;
        wall = Object.Instantiate(wall);

        yield return new WaitForSeconds(1);

        Assert.IsNull(GameObject.FindWithTag("Bullet"));
        Object.Destroy(wall);
    }

    [UnityTest]
    public IEnumerator CheckProjectileCollisionWithEnemy() {
        
        Assert.IsNotNull(GameObject.FindWithTag("Bullet"));

        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        enemy.GetComponent<Transform>().position = projectile.GetComponent<Transform>().position;
        enemy = Object.Instantiate(enemy);

        yield return new WaitForSeconds(1);

        Assert.IsNull(GameObject.FindWithTag("Bullet"));
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator CheckProjectileCollisionWithNPC() {
        Assert.IsNotNull(GameObject.FindWithTag("Bullet"));

        GameObject npc = Resources.Load<GameObject>("Prefabs/NPC");
        npc.GetComponent<Transform>().position = projectile.GetComponent<Transform>().position;
        npc = Object.Instantiate(npc);

        yield return new WaitForSeconds(1);

        Assert.IsNull(GameObject.FindWithTag("Bullet"));

        Object.Destroy(npc);
    }

    [UnityTest]
    public IEnumerator CheckProjectileOrigin() {
        
        Object.Destroy(projectile.gameObject);

        player.GetComponent<PlayerController>().Construct();
        player.GetComponent<PlayerController>().Fire();

        yield return new WaitForSeconds(1);

        Assert.True(player.GetComponent<Transform>().position == GameObject.FindWithTag("Bullet").GetComponent<Transform>().position);
    }

    [UnityTest]
    public IEnumerator CheckProjectileDirection1() {
        
        var vectX = 1;
        var vectY = 1;
        var vect = new Vector3(vectX, vectY, 0);

        projectile.GetComponent<P1BulletShot>().CalculateBulletMovement(vect);

        var testVectX = projectile.GetComponent<Rigidbody2D>().velocity.x;
        var testVectY = projectile.GetComponent<Rigidbody2D>().velocity.y;
        var testVect = new Vector3(testVectX, testVectY, 0);

        vect.Normalize();
        testVect.Normalize();

        Assert.True(Mathf.Abs(vect.x - testVect.x) < 0.000001);
        Assert.True(Mathf.Abs(vect.y - testVect.y) < 0.000001);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckProjectileSpeed() {
        
        var vectX = 1;
        var vectY = 1;
        var vect = new Vector3(vectX, vectY, 0);

        projectile.GetComponent<P1BulletShot>().CalculateBulletMovement(vect);

        Assert.NotZero(projectile.GetComponent<Rigidbody2D>().velocity.magnitude);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckProjectileLifetime() {
        
        projectile.GetComponent<P1BulletShot>().CalculateBulletMovement(new Vector3(1, 1, 0));
        yield return new WaitForSeconds(1.1f);

        Assert.IsNull(GameObject.FindWithTag("Bullet"));

    }

    [UnityTest]
    public IEnumerator CheckProjectileType() {
        
        Assert.True(projectile.GetComponent<P1BulletShot>().GetBulletType().Equals("Default"));
        yield return null;

    }

    [UnityTest]
    public IEnumerator CheckProjectileSprite() {
        
        projectile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/bulleta");
        yield return null;

    }


}