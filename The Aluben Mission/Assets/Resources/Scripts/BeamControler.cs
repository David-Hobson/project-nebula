using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamControler : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject beamSprite;

    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        beamSprite = this.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        CalculateBeamPosition();
	}

    private void CalculateBeamPosition() {
        var centerPoint = player1.transform.position + (player2.transform.position - player1.transform.position) / 2;
        var playerDistance = Vector2.Distance(player1.transform.position, player2.transform.position);

        //this.GetComponent<LineRenderer>().SetPositions(new[] { player1.transform.position, player2.transform.position });
        this.transform.position = centerPoint;
        this.GetComponent<BoxCollider2D>().size = new Vector2(0.06f, playerDistance);
        this.transform.position = centerPoint;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, player1.transform.position - player2.transform.position);


        float spriteSize = beamSprite.GetComponent<SpriteRenderer>().sprite.rect.height / beamSprite.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        Vector3 scale = transform.localScale;
        scale.y = playerDistance / spriteSize;

        beamSprite.transform.localScale = scale;

    }

}
