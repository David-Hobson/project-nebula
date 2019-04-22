using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeWorldIntro : MonoBehaviour {


    private float timing;
    private float fadeTime;
    private int dialogueIndex;

    private int state;
    private int playerState;

    private GameObject player1;
    public Sprite[] player1Sprites;

    private GameObject player2;
    public Sprite[] player2Sprites;

    public GameObject[] dialogues;

    public GameObject dialogueManager;
    private GameObject fadeIn;

    private float dialogueElapsed;


    // Use this for initialization
    void Start() {
        timing = 0.0f;
        fadeTime = 0.0f;
        state = 0;
        dialogueIndex = 0;
        dialogueElapsed = 0;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        fadeIn = GameObject.Find("FadeIn");
    }

    // Update is called once per frame
    void Update() {
        timing += Time.deltaTime;

        if (state == 0) {
            FadeIn();
        } else if (state == 1) {
            TurnPlayers();
        } else if (state == 2) {
            if (dialogueIndex >= dialogues.Length) {
                timing += Time.deltaTime;
                if (timing >= 2.0f) {
                    state++;
                    timing = 0;
                }

            } else {
                RunDialog(dialogues[dialogueIndex].GetComponent<Dialogue>());
            }
        } else if (state == 3) {
            FadeOut();
        } else if (state == 4) {
            SceneManager.LoadScene("MainMenu");
        } else if (state == 5) {

        } else if (state == 6) {

        } else if (state == 7) {

        }



    }

    private void RunDialog(Dialogue dialogue) {
        if (dialogueManager.GetComponent<DialogueManager>().IsFinished()) {
            dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue);
        } else {
            dialogueElapsed += 0.01f;
            if (dialogueElapsed >= 3.0f) {
                dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
                dialogueElapsed = 0.0f;

                if (dialogueManager.GetComponent<DialogueManager>().IsFinished()) {
                    dialogueIndex++;
                }
            }

        }

    }

    private void FadeIn(){
        if(timing >= 2.0f){
            fadeTime += 0.01f;
            fadeIn.GetComponent<Image>().color = new Color(0, 0, 0, 1 - fadeTime);
        }

        if(fadeTime >= 1.0f){
            state++;
            timing = 0;
            fadeTime = 0;
        }
    }

    private void FadeOut(){
        if (timing >= 2.0f) {
            fadeTime += 0.01f;
            fadeIn.GetComponent<Image>().color = new Color(0, 0, 0, fadeTime);
        }

        if (fadeTime >= 1.0f) {
            state++;
            timing = 0;
        }
    }

    private void TurnPlayers(){
        if(timing >= 1.0f){
            player1.GetComponent<SpriteRenderer>().sprite = player1Sprites[playerState];
            player2.GetComponent<SpriteRenderer>().sprite = player2Sprites[playerState];
            playerState++;
            timing = 0.0f;
        }

        if(playerState >= player1Sprites.Length){
            state++;
        }

    }

}
