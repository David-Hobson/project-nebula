using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public GameObject music;
    private int state;
    public float fadeTime;
    public float timing;

	// Use this for initialization
	void Start () {
        music = GameObject.Find("Music");
        state = 0;
        fadeTime = 0;
        timing = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timing += Time.deltaTime;

        if(state == 0){
            Fade();
        }else if (state == 1){
            music.GetComponent<AudioSource>().Play();
            state++;
        }
	}

    private void Fade() {
        if (timing >= 2.0f) {
            fadeTime += 0.01f;
            this.GetComponent<Image>().color = new Color(0, 0, 0, 1 - fadeTime);
        }

        if (fadeTime >= 1.0f) {
            state++;
            timing = 0;
            fadeTime = 0;
        }
    }
}
