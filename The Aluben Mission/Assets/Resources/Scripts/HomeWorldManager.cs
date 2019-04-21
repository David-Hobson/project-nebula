using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeWorldManager : MonoBehaviour
{

    private GameObject player1;
    private GameObject player2;
    private GameObject dialogueManager;
    private GameObject shop;
    private GameObject HUDController;
    private bool menutoggle = false;
    private int player;


    // Use this for initialization
    void Start()
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        dialogueManager = GameObject.Find("DialogueManager");
        shop = GameObject.Find("Shop");
        HUDController = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {

        float vect = Vector3.Distance(this.transform.position, player1.transform.position);
        float vect2 = Vector3.Distance(this.transform.position, player1.transform.position);

        if (shop.GetComponent<Shop>().isClosed() && menutoggle)
        {
            menutoggle = false;
            HUDController.SetActive(true);
            player1.GetComponent<PlayerController>().SetInDialogue(false);
        }
        else if (vect < 1.16f && player1.GetComponent<PlayerController>().GetInteraction())
        {
            player = 1;
            if (this.GetComponent<Dialogue>())
            {
                this.RunDialogue();
            }

            if (this.GetComponent<LevelChange>())
            {
                SceneManager.LoadScene(this.GetComponent<LevelChange>().GetScene());
            }
        }
        else if(vect2 < 1.16f && player2.GetComponent<PlayerController>().GetInteraction())
        {
            player = 2;
            if (this.GetComponent<Dialogue>())
            {
                this.RunDialogue();
            }

            if (this.GetComponent<LevelChange>())
            {
                SceneManager.LoadScene(this.GetComponent<LevelChange>().GetScene());
            }
        }
    }


    //Manages the dialogue events throughout the homeworld.
    void RunDialogue()
    {
        if(player == 1)
        {
            if (!player1.GetComponent<PlayerController>().GetInDialogue())
            {
                player1.GetComponent<PlayerController>().SetInDialogue(true);
                dialogueManager.GetComponent<DialogueManager>().StartDialogue(this.GetComponent<Dialogue>());
            }
            else
            {
                dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            }

            if (dialogueManager.GetComponent<DialogueManager>().IsFinished())
            {
                if (dialogueManager.GetComponent<DialogueManager>().getName() == "Tobs")
                {
                    HUDController.SetActive(false);
                    OpenShop(1);
                    player1.GetComponent<PlayerController>().SetInDialogue(true);
                }
                else
                    player1.GetComponent<PlayerController>().SetInDialogue(false);
            }
            
        }
        else { 
            if (!player2.GetComponent<PlayerController>().GetInDialogue())
            {
                player2.GetComponent<PlayerController>().SetInDialogue(true);
                dialogueManager.GetComponent<DialogueManager>().StartDialogue(this.GetComponent<Dialogue>());
            }
            else
            {
                dialogueManager.GetComponent<DialogueManager>().DisplayNextSentence();
            }

            if (dialogueManager.GetComponent<DialogueManager>().IsFinished())
            {
                if (dialogueManager.GetComponent<DialogueManager>().getName() == "Tobs")
                {
                    HUDController.SetActive(false);
                    OpenShop(2);
                    player2.GetComponent<PlayerController>().SetInDialogue(true);
                } else
                    player2.GetComponent<PlayerController>().SetInDialogue(false);
            }
        }
    }

    void OpenShop(int player)
    {
        if (!menutoggle) { 
            menutoggle = true;
            shop.GetComponent<Shop>().openShop(player);
        }
    }

}   
