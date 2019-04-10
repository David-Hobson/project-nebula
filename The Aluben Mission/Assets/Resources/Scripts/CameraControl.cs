using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class CameraControl : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;

	//Initialize by finding both players on the screen
	void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
	}
	
	void Update () {
        if (player1 == null && player2 == null) {
            SceneManager.LoadScene("GameOver");
        }

        var baseCameraPosition = CalculateCameraPosition();
        //var offsetCameraPosition = CalculateCameraOffset();

        //Debug.Log(offsetCameraPosition.x + " | " + offsetCameraPosition.y);

        //transform.position = new Vector3(baseCameraPosition.x + offsetCameraPosition.x * 0.5f, baseCameraPosition.y + offsetCameraPosition.y * 0.5f, -1);
        transform.position = new Vector3(baseCameraPosition.x, baseCameraPosition.y, -1);
	}

    //REQUIREMENT: F-53
    //Calculate the camera position as the midpoint between two players
    public Vector3 CalculateCameraPosition(){
        Vector3 vect;

        if (player1 == null) {
            //Follow player 2 if player 1 cannot be found in the scene
            vect = player2.GetComponent<Transform>().position;
            //Check scene to find player 1
            player1 = GameObject.Find("Player 1");
        } else if (player2 == null) {
            //Follow player 1 if player 2 cannot be found in the scene
            vect = player1.GetComponent<Transform>().position;
            //Check scene to find player 2
            player2 = GameObject.Find("Player 2");
        } else {
            //Calculate midpoint between both players
            vect = player1.GetComponent<Transform>().position + (player2.GetComponent<Transform>().position - player1.GetComponent<Transform>().position) / 2;
        }

        return vect;
    }

    public Vector3 CalculateCameraOffset(){
        return (player1.transform.GetChild(0).transform.rotation * Vector3.down).normalized;
    }
}
