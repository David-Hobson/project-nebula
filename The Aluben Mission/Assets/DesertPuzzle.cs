using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzle : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject beam;

    // Use this for initialization
    void Start() {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        beam = GameObject.Find("Beam");
    }

    // Update is called once per frame
    void Update() {
        var centerPoint = player1.transform.position + (player2.transform.position - player1.transform.position) / 2;

        beam.GetComponent<LineRenderer>().SetPositions(new[] { player1.transform.position, player2.transform.position });
        beam.GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(player1.transform.position, player2.transform.position), 0.06f);
        beam.transform.position = centerPoint;
        Debug.Log(Vector3.Angle(player1.transform.position, player2.transform.position));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Beamable") {
            Debug.Log("BEAMING!!");
        }
    }

}
