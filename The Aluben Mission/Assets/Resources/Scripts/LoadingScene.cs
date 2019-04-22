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
        if(this.name == "Thaelia"){
            if (timeToLoad >= 5.0f) {
                SceneManager.LoadScene("SpiritWorld2");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }else if(this.name == "Casurn"){
            if (timeToLoad >= 5.0f) {
                SceneManager.LoadScene("DesertWorld");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }else if(this.name == "ShadowBoss"){
            if (timeToLoad >= 5.0f) {
                SceneManager.LoadScene("ShadowBoss");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }else if(this.name == "DesertBoss"){
            if (timeToLoad >= 5.0f) {
                SceneManager.LoadScene("DesertBoss");
            } else {
                timeToLoad += Time.deltaTime;
            }
        }

	}
}
