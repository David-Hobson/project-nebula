using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour {

    public GameObject path;

    private int currentDirection;
    private float nodeRange;

	// Use this for initialization
	void Start () {

        currentDirection = 0;
        nodeRange = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
        this.CalculateMovement();
	}

    private void CalculateMovement() {
        Vector2 nodePosition = path.transform.GetChild(currentDirection).transform.position;
        Vector2 direction = nodePosition - (Vector2)transform.position;

        direction = direction.normalized;
        this.transform.GetComponent<Rigidbody2D>().velocity = direction * 0.7f;

        bool boundsX = this.transform.position.x <= nodePosition.x + nodeRange && this.transform.position.x >= nodePosition.x - nodeRange;
        bool boundsY = this.transform.position.y <= nodePosition.y + nodeRange && this.transform.position.y >= nodePosition.y - nodeRange;

        if (boundsX && boundsY) {
            currentDirection++;
            if (currentDirection >= path.transform.childCount) {
                currentDirection = 0;
            }
        }
    }
}
