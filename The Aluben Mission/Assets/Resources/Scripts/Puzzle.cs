using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public int PuzzleStart()
    {
        return 1;
    }

    public int PuzzleInProgress()
    {
        return 2;
    }

    public int PuzzleCompleted()
    {
        return 3;
    }

    public int PuzzleInProgressState()
    {
        return 4;
    }

    public int PuzzleFailed()
    {
        return 5;
    }

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
