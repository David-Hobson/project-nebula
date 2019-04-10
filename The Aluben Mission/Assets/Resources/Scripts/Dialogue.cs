using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//information about a single dialogue
[System.Serializable]
public class Dialogue : MonoBehaviour {
    /*
     * Stores dialog information for dialogue boxes throughout the game.
    */

    public string name;

    [TextArea(3,10)]
    public string[] sentences;

    public Dialogue(string name, string[] sentences){
        this.name = name;
        this.sentences = sentences;
    }
}
