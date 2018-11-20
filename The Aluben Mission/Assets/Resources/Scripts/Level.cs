using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public int LevelActive()
    {
        return 1;
    }

    public int LevelNotComplete()
    {
        return 2;
    }

    public int LevelComplete()
    {
        return 3;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
