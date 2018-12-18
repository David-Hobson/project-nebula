using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour {

    public int unlocked = 1;
    public int locked = 5;
    public bool correction = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool mapActive() {
        return true;
    }

    public int unlockedLevel()
    {
        return unlocked;
    }

    public int unlockedWorld()
    {
        return unlocked;
    }

    public int lockedWorld()
    {
        return locked;
    }

    public int lockedLevel()
    {
        return locked;
    }

    public bool mapCorrection()
    {
        return correction;
    }

    public bool worldMapUpdate()
    {
        return true;
    }
}
