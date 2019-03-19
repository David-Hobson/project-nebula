using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        float vect = Vector3.Distance(this.transform.position, player1.transform.position);


        if (vect < 1.16f && player1.GetComponent<Player1Controller>().GetInteraction()) {
            if(this.GetComponent<Dialogue>()){
                this.RunDialogue();
            }
        }
	}

    void RunDialogue(){
        if(!player1.GetComponent<Player1Controller>().GetInDialogue()){
            player1.GetComponent<Player1Controller>().SetInDialogue(true);
            dialogueManager.GetComponent<DialogueManager>().StartDialogue(this.GetComponent<Dialogue>());
        }else{
            dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
        }

        if(dialogueManager.GetComponent<DialogueManager>().IsFinished()){
            player1.GetComponent<Player1Controller>().SetInDialogue(false);
        }
    }


}
