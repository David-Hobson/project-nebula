using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertChallenge : MonoBehaviour {

    public GameObject barrier;
    private GameObject[] orbs;
    private bool completed;

    private GameObject puzzle;

    private float completeTime;

	// Use this for initialization
	void Start () {
        completed = true;
        completeTime = 0;

        puzzle = GameObject.Find("DesertPuzzle");

        orbs = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++){
            orbs[i] = this.transform.GetChild(i).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
        completed = true;
        for (int i = 0; i < orbs.Length; i ++){
            completed = completed && orbs[i].GetComponent<BeamableObject>().IsCharging();
        }

        if(completed){
            completeTime += Time.deltaTime;
            if(completeTime >= 2.0f){
                for (int i = 0; i < orbs.Length; i++) {
                    orbs[i].GetComponent<BeamableObject>().SetComplete();
                }
                puzzle.GetComponent<DesertPuzzle>().FinishChallenge(barrier);
                this.GetComponent<AudioSource>().Play();
            }
        }else{
            completeTime = 0;
        }
	}
}
