using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastOtb : MonoBehaviour {


    private int state;
    private int currentDirection;

    private float chargingTime;

    private GameObject energize;
    public GameObject path;

    private float nodeRange;

    public Sprite chargedOrb;
    public Sprite unchargedOrb;

    // Use this for initialization
    void Start() {

        energize = this.transform.GetChild(0).gameObject;

        state = 0;
        currentDirection = 0;

        nodeRange = 0.01f;

    }

    // Update is called once per frame
    void Update() {
        if (state == 0) {
            this.Uncharged();
        } else if (state == 1) {
            this.Charging();
        } else if (state == 2) {
            this.Charged();
        }

    }

    private void Uncharged() {
        this.GetComponent<SpriteRenderer>().sprite = unchargedOrb;
        energize.GetComponent<SpriteRenderer>().enabled = false;

    }

    private void Charging() {
        energize.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Charged() {
        this.GetComponent<SpriteRenderer>().sprite = chargedOrb;
        energize.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Beam" && state == 0) {
            state = 1;
        }

    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Beam" && state == 1) {
            state = 0;
        }
    }

    public void SetComplete() {
        state = 2;
    }

    public bool IsCharging() {
        return state == 1;
    }

    private void Forward() {
        Vector2 nodePosition = path.transform.GetChild(currentDirection).transform.position;

        if (currentDirection >= path.transform.childCount - 1) {
            this.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        } else {
            nodePosition = path.transform.GetChild(currentDirection).transform.position;
            Vector2 direction = nodePosition - (Vector2)transform.position;

            direction = direction.normalized;
            this.transform.GetComponent<Rigidbody2D>().velocity = direction * 0.2f;
        }

        bool boundsX = this.transform.position.x <= nodePosition.x + nodeRange && this.transform.position.x >= nodePosition.x - nodeRange;
        bool boundsY = this.transform.position.y <= nodePosition.y + nodeRange && this.transform.position.y >= nodePosition.y - nodeRange;

        if (boundsX && boundsY) {
            currentDirection++;
            if (currentDirection >= path.transform.childCount) {
                currentDirection = path.transform.childCount - 1;
            }
        }
    }

    private void Reverse() {
        Vector2 nodePosition = path.transform.GetChild(currentDirection).transform.position;

        if (currentDirection <= 0) {
            this.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        } else {
            nodePosition = path.transform.GetChild(currentDirection - 1).transform.position;
            Vector2 direction = nodePosition - (Vector2)transform.position;

            direction = direction.normalized;
            this.transform.GetComponent<Rigidbody2D>().velocity = direction * 1.0f;
        }


        bool boundsX = this.transform.position.x <= nodePosition.x + nodeRange && this.transform.position.x >= nodePosition.x - nodeRange;
        bool boundsY = this.transform.position.y <= nodePosition.y + nodeRange && this.transform.position.y >= nodePosition.y - nodeRange;

        if (boundsX && boundsY) {
            currentDirection--;
            if (currentDirection <= 0) {
                currentDirection = 0;
            }
        }
    }

}