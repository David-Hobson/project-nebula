using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour {

    public Transform Target;
    Vector3 velocity = Vector3.zero;
    bool isFollowing = false;

	// Use this for initialization
	void Start () {
		
	}
	
    public void StartFollowing()
    {
        isFollowing = true;
    }
	// Update is called once per frame
	void Update () {
        if(isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref velocity, 0.2f);
        }
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
