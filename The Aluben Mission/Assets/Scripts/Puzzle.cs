using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//F-31
	public bool isComplete(bool key){
		if(key == true){
			return true;
		}else
			return false;
	}
}
}

