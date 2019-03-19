using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {


    private float timeToLoad;

	// Use this for initialization
	void Start () {
        timeToLoad = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(this.name == "Travelling"){
            if (timeToLoad <= 20.0f) {
                SceneManager.LoadScene("SpiritForestWorld");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }else{
            if (timeToLoad <= 50.0f) {
                SceneManager.LoadScene("ShadowBoss");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }

	}
}
