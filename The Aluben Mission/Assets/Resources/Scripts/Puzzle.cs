using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    //FR-54
    public int PuzzleStart()
    {
        return 1;
    }
    //FR-54
    public int PuzzleInProgress()
    {
        return 2;
    }
    //FR-31
    public int PuzzleCompleted()
    {
        return 3;
    }
    //FR-54
    public int PuzzleInProgressState()
    {
        return 4;
    }
    //FR-55
    public int PuzzleFailed()
    {
        return 5;
    }
    //FR-56
    public int PuzzleRewards()
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
