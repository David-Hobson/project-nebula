using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeWorldManager : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject dialogueManager;


    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        dialogueManager = GameObject.Find("DialogueManager");
    }
	
	// Update is called once per frame
	void Update () {

        float vect1 = Vector3.Distance(this.transform.position, player1.transform.position);
        float vect2 = Vector3.Distance(this.transform.position, player2.transform.position);


        if (Mathf.Min(vect1, vect2) < 1.16f && (player1.GetComponent<PlayerController>().GetInteraction() || player2.GetComponent<PlayerController>().GetInteraction())) {
            if(this.GetComponent<Dialogue>()){
                this.RunDialogue();
            }

            if(this.GetComponent<LevelChange>()){
                SceneManager.LoadScene(this.GetComponent<LevelChange>().GetScene());
            }
        }
	}

    //Manages the dialogue events throughout the homeworld.
    void RunDialogue(){
        if(!player1.GetComponent<PlayerController>().GetInDialogue() && !player2.GetComponent<PlayerController>().GetInDialogue()) {
            player1.GetComponent<PlayerController>().SetInDialogue(true);
            player2.GetComponent<PlayerController>().SetInDialogue(true);

            dialogueManager.GetComponent<DialogueManager>().StartDialogue(this.GetComponent<Dialogue>());
        }else{
            dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }

        if(dialogueManager.GetComponent<DialogueManager>().IsFinished()){
            player1.GetComponent<PlayerController>().SetInDialogue(false);
            player2.GetComponent<PlayerController>().SetInDialogue(false);
        }
    }


}
