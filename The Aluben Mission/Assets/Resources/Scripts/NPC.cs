using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    bool isTalking; //is the NPC currently talking

    //FR 22
    /* to be implemented:
     * Actions that happen when the player interacts with the NPC
     */
    public void startDialogue() { }

    //FR 22
    /* to be implemented:
     * Actions that happen when the player stops interacting with the NPC
     */
    public void endDialogue() { }

    public bool checkStatus() { return isTalking; } //check if the NPC is still active

}

