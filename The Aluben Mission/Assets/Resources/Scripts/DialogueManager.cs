﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    /*
     * Manges how all dialogue is controller throughout the game
    */

    public Text nameText;
    public Text dialogueText;

    public bool finishedDialogue;

    public Animator animator;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
    public void StartDialogue (Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        this.finishedDialogue = false;

        foreach (string sentence in dialogue.sentences )
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray ())
        {
            dialogueText.text += letter;
            yield return null; 
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        this.finishedDialogue = true;
    }

    public bool IsFinished(){
        return this.finishedDialogue;
    }
}