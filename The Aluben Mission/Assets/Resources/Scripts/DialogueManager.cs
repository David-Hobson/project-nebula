using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    /*
     * Manges how all dialogue is controller throughout the game
    */

    public Text nameText;
    public Text dialogueText;

    public AudioClip enableDialogueSound;
    public AudioClip dialogueReadoutSound;

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

        this.GetComponent<AudioSource>().PlayOneShot(enableDialogueSound, 0.8f);

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
        this.GetComponent<AudioSource>().PlayOneShot(dialogueReadoutSound, 0.5f);
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; 
        }
        this.GetComponent<AudioSource>().Stop();
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        this.finishedDialogue = true;
    }

    public string getName()
    {
        return this.nameText.text;
    }

    public bool IsFinished(){
        return this.finishedDialogue;
    }
}
