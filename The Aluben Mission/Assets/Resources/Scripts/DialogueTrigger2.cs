using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{
    public Dialogue2 dialogue2;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager2>().StartDialogue(dialogue2);
    }

}
