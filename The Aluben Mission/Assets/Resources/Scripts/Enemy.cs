using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform player;
    public int health = 100;
    private float damaged;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        if(player != null){
            Vector3 vect = player.position - transform.position;

            if(vect.magnitude <= 2){
                this.GetComponent<Rigidbody2D>().velocity = vect*0.5f;
            }

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
        }
    }
}
