using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour {

    public Transform Target; // Target is player 1 or player 2
    private float speed = 1f; // movement speed of dropped items
    bool isFollowing = false; // is animation finished

    //check the animation of item is finished, if its finished, starts to follow players
    public void StartFollowing()
    {
        isFollowing = true;
    }

    //make the item chase the players
    public void Following()
    {
        Vector3 vect = Target.position - transform.position;
        if (vect.magnitude <= 1)
        {
            if(isFollowing)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
            }
        }           
    }

    void FixedUpdate()
    {
        Following();
    }

    //if its collides with player, destroy the item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
