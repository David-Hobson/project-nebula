using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vect = player.position - transform.position;
        if (vect.magnitude <= 1) {
            SceneManager.LoadScene("EndDemo");
        }
    }
}
