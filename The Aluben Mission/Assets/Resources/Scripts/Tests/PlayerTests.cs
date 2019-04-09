using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerTests {

    public GameObject player;

    [SetUp]
    public void SetUp() {
        // Use the Assert class to test conditions.
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        player.GetComponent<PlayerController>().Construct();
    }

    [TearDown]
    public void TearDown() {
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator CheckPlayerIsAlive() {
        
        Assert.True(player.GetComponent<PlayerController>().GetHealth() > 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerIsDead() {
        
        player.GetComponent<PlayerController>().Damage(150, new Vector3(1,1,0));
        Assert.True(player.GetComponent<PlayerController>().GetHealth() <= 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerRespawns() {
        
        Assert.NotNull(GameObject.FindWithTag("Player"));


        var newPlayer = player.GetComponent<PlayerController>().Damage(150, new Vector3(1, 1, 0));
        if(newPlayer != null){
            player = newPlayer;
        }

        yield return new WaitForSeconds(5);
        Assert.Null(GameObject.FindWithTag("Player"));

        player.GetComponent<PlayerController>().Respawn();
        Assert.NotNull(GameObject.FindWithTag("Player"));

    }

    [UnityTest]
    public IEnumerator CheckPlayerMovement() {
        

        var currentPosition = player.GetComponent<Transform>().position;
        for (var i = 0; i < 100; i ++){
            player.GetComponent<PlayerController>().CalculateMovement(-1, 1);
            yield return new WaitForSeconds(0.1f);
        }

        Assert.True(currentPosition != player.GetComponent<Transform>().position);
    }

    [UnityTest]
    public IEnumerator CheckPlayerAnimation() {


        var currentSprite = player.GetComponent<SpriteRenderer>().sprite;

        for (var i = 0; i < 100; i++) {
            //player.GetComponent<PlayerController>().MovementAnimation(-1, 1);
            yield return new WaitForSeconds(0.1f);
        }

        Assert.True(currentSprite != player.GetComponent<SpriteRenderer>().sprite);
    }

    [UnityTest]
    public IEnumerator CheckPlayerCollisionWithWall() {
        var wall = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Wall"));

        wall.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x + 0.5f, player.GetComponent<Transform>().position.y, 0);

        for (int i = 0; i < 100; i++){
            player.GetComponent<PlayerController>().CalculateMovement(1, 0);
        }

        Assert.False(player.GetComponent<Transform>().position.x >= wall.GetComponent<Transform>().position.x);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerCollisionWithEnemy() {
        var enemy = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"));

        enemy.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x + 0.5f, player.GetComponent<Transform>().position.y, 0);

        for (int i = 0; i < 50; i++) {
            player.GetComponent<PlayerController>().CalculateMovement(1, 0);
        }

        Assert.False(player.GetComponent<Transform>().position.x >= enemy.GetComponent<Transform>().position.x);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerUpgradeHealth() {

        player.GetComponent<PlayerController>().Upgrade(1);

        Assert.True(player.GetComponent<PlayerController>().GetMaxHealth() == 200);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerUpgradeArmour() {

        player.GetComponent<PlayerController>().Upgrade(2);

        Assert.True(player.GetComponent<PlayerController>().GetMaxArmour() == 150);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerUpgradeSpeed() {
        player.GetComponent<PlayerController>().Upgrade(3);

        Assert.True(player.GetComponent<PlayerController>().GetSpeed() == 1.5f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerSpeed() {
        Assert.True(player.GetComponent<PlayerController>().GetSpeed() == 1f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerDodge() {
        Assert.True(player.GetComponent<PlayerController>().Dodge() == "Dodging");
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckPlayerDamaged() {

        var currentHealth = player.GetComponent<PlayerController>().GetHealth();

        player.GetComponent<PlayerController>().Damage(10, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(1);

        Assert.True(currentHealth >= player.GetComponent<PlayerController>().GetHealth());

    }

    [UnityTest]
    public IEnumerator CheckPlayerArmourDamage() {

        var currentArmour = player.GetComponent<PlayerController>().GetArmour();

        player.GetComponent<PlayerController>().DamageArmour(10);
        yield return new WaitForSeconds(1);

        Assert.True(currentArmour >= player.GetComponent<PlayerController>().GetArmour());

    }

    [UnityTest]
    public IEnumerator CheckPlayerHeal() {

        var currentHealth = player.GetComponent<PlayerController>().GetHealth();

        player.GetComponent<PlayerController>().Damage(10, new Vector3(1, 1, 0));
        player.GetComponent<PlayerController>().Heal(10);
        yield return new WaitForSeconds(1);
          
        Assert.True(currentHealth <= player.GetComponent<PlayerController>().GetHealth());
    }

    [UnityTest]
    public IEnumerator CheckPlayerHealArmour() {

        var currentHealth = player.GetComponent<PlayerController>().GetHealth();

        player.GetComponent<PlayerController>().DamageArmour(10);
        player.GetComponent<PlayerController>().HealArmour(10);
        yield return new WaitForSeconds(1);

        Assert.True(currentHealth <= player.GetComponent<PlayerController>().GetHealth());
    }
}


