 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    /*
     * Controls the main scenes for the game.
    */


	public bool click = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	public enum GameState{
		mainMenu,
		pause,
		cutScene,
		worldMap,
		level,
		gameOver
	}
	public static GameState state = GameState.mainMenu;

	public void CutScene(string cScene){
		SceneManager.LoadScene (cScene);
	}

	public void PlayerStatus(int vol){
		AudioListener.volume = vol;
	}

	//public void volChange(int vol){
	//	AudioListener.volume = vol;
	//}

	public void Menu(){
		string curScene = SceneManager.GetActiveScene ().name;
		if (click == true) {
			SceneManager.LoadScene ("Menu");
		} else {
			SceneManager.LoadScene (curScene);
		}
	}


	public bool ActionLog(){
		//Potentially Dropping the Action Log
		var implemented = false;
		return implemented;
	}

	public bool isPaused(bool p){
		if (p == true) {
			Time.timeScale = 0;
			return true;
		} else {
			Time.timeScale = 1;
			return false;
		}
	}


		
}
