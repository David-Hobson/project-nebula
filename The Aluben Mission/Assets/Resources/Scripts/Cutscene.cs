using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

    public int CutsceneStart(){
        return 1;
    }

    public int CutsceneCompleted()
    {
        return 2;
    }

    public int CutscenePauseSelection()
    {
        return 3;
    }

    public int CutsceneUnpauseSelection()
    {
        return 4;
    }

    public int CutsceneSkipSelection()
    {
        return 5;
    }

    public int CutsceneResumeSelection()
    {
        return 6;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
