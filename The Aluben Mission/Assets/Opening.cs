using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {


    private float timing;
    private float fadeTime;
    private int textIndex;
    private int dialogueIndex;

    private float textFadeSpeed;
    private float fadeElapsed;

    private int state;

    public GameObject[] textSet;
    public GameObject[] dialogues;
    public GameObject[] dialogues2;

    public GameObject spaceship;
    public GameObject shipPosition;

    private GameObject background;

    public GameObject dialogueManager;

    private float dialogueElapsed;

    private GameObject mainCam;
    private GameObject particles;

    public AudioClip powerDown;

	// Use this for initialization
	void Start () {
        timing = 0.0f;
        fadeTime = 0.0f;
        textIndex = 0;
        textFadeSpeed = 0.01f;
        fadeElapsed = 0.0f;
        state = 0;
        dialogueIndex = 0;

        dialogueElapsed = 0;

        background = GameObject.Find("Background");
        mainCam = GameObject.Find("Main Camera");
        particles = GameObject.Find("Particle System");
	}
	
	// Update is called once per frame
	void Update () {

        if(state == 0){
            timing += Time.deltaTime;

            if (timing >= 1.0f) {
                FadeText(textSet[textIndex], 2.0f);
            }

            if (textIndex >= textSet.Length) {
                state++;
            }
        } else if(state == 1){
            FadeBackground();
        } else if(state == 2){
            if(!this.GetComponent<AudioSource>().isPlaying){
                this.GetComponent<AudioSource>().Play();
            }
            MoveSpaceship();
        } else if (state == 3){

            if(dialogueIndex >= dialogues.Length){
                timing += Time.deltaTime;
                if(timing >= 2.0f){
                    state++;
                    timing = 0;
                }

            }else{
                RunDialog(dialogues[dialogueIndex].GetComponent<Dialogue>());
            }

        }else if(state == 4){
            background.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            mainCam.GetComponent<Camera>().backgroundColor = new Color(0, 0, 0, 1);
            spaceship.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            particles.SetActive(false);
            state++;
            dialogueIndex = 0;
            timing = 0.0f;
            this.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().PlayOneShot(powerDown);
        } else if(state == 5){
            timing += Time.deltaTime;

            if(timing >= 2.5f){
                state++;
            }

        }else if(state == 6){
            if (dialogueIndex >= dialogues2.Length) {
                timing += Time.deltaTime;
                if (timing >= 2.0f) {
                    state++;
                }

            } else {
                RunDialog(dialogues2[dialogueIndex].GetComponent<Dialogue>());
            }
        }else if(state == 7){
            SceneManager.LoadScene("HomeWorldIntro");
        }

	}

    private void RunDialog(Dialogue dialogue){
        if(dialogueManager.GetComponent<DialogueManager>().IsFinished()){
            dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
        }else{
            dialogueElapsed += 0.01f;
            if(dialogueElapsed >= 3.0f){
                dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
                dialogueElapsed = 0.0f;

                if(dialogueManager.GetComponent<DialogueManager>().IsFinished()){
                    dialogueIndex++;
                }
            }

        }

    }

    private void MoveSpaceship(){
        fadeTime += textFadeSpeed;
        spaceship.transform.position = Vector3.MoveTowards(spaceship.transform.position, shipPosition.transform.position, fadeTime);

        if (fadeTime >= 2.0f) {
            state++;
            fadeTime = 0;
            timing = 0;
        }
    }

    private void FadeBackground(){
        fadeTime += textFadeSpeed;
        background.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeTime);

        if(fadeTime >= 1.0){
            state++;
            fadeTime = 0;
        }
    }

    private void FadeText(GameObject changeText, float elapsed){
        fadeTime += textFadeSpeed;

        changeText.GetComponent<Text>().color = new Color(0, 0, 0, fadeTime);

        if(fadeTime >= 1.0f){
            fadeTime = 1.0f;
            fadeElapsed += Time.deltaTime;
        }

        if(fadeElapsed > elapsed){
            textFadeSpeed = -0.01f;
        }

        if(fadeTime < 0.0f){
            fadeTime = 0.0f;
            timing = 0;
            textIndex++;
            fadeElapsed = 0;
            textFadeSpeed = 0.01f;
            return;
        }

    }
}
