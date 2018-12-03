using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform player;
    public GameObject crystalPrefab;
    public int speed;

    public int health = 100;
    private float damaged;

    public int droppedItem = 0;
    public bool collisionWithObjets = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        if(player != null){
            Vector3 vect = player.position - transform.position;
            if(vect.magnitude <= 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed*0.01f);
            }
            /*
            if(vect.magnitude <= 2){
                this.GetComponent<Rigidbody2D>().velocity = vect*0.5f;
            }
            */
            this.GetComponent<Animator>().SetFloat("EnemySpeedX", vect.x);
            this.GetComponent<Animator>().SetFloat("EnemySpeedY", vect.y);

            this.GetComponent<SpriteRenderer>().color = new Color(255, damaged, damaged);
            damaged += 0.1f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            health -= 25;
            Destroy(other.gameObject);
            damaged = 0;
        }

        if(health <= 0){
            Destroy(gameObject);
            var go = Instantiate(crystalPrefab, transform.position, Quaternion.identity);
            go.GetComponent<DroppedItem>().Target = player;
        }
    }

    public int DropItem() {

        droppedItem = 1;
        return droppedItem;
    }

    public int EnemyRespawns(int number) {
        return number;
    }

    public int EnemyHealed(int number) {
        health = health + number;
        return health;
    }
}
