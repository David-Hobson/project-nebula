using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController_Tests {

 //  [Test]
 // public void GameController_TestsSimplePasses() {
        // Use the Assert class to test conditions.
//		Assert.AreEqual (true, true);
//   }

	//Done
    [UnityTest]
    public IEnumerator CheckGameState() {
		string curState = "none";
		bool checkState = false;
		var stateController = new GameController ();

		if (curState == "setState") {
			checkState = true;
		} else {
		}

		yield return null;
		Assert.AreEqual (true, checkState);
	}

	[UnityTest]
	public IEnumerator CheckInitialState() {


		yield return null;
		Assert.AreEqual (true, false);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckCutscenePause() {
		bool isNewScene = false;

		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;

		var gamecontroller = new GameController();
		gamecontroller.CutScene ("EndDemo");

		bool paused = gamecontroller.isPaused (true);

		if (sceneName == "EndDemo" && paused == true) {
			isNewScene = true;
		} else {
			isNewScene = false;
		}

		yield return null;
		Assert.AreEqual (true, isNewScene);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckCutsceneStop() {
		bool isNewScene = false;

		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;
			
		var gamecontroller = new GameController();
		gamecontroller.CutScene ("EndDemo");

		if (sceneName == "EndDemo") {
			isNewScene = true;
		} else {
			isNewScene = false;
		}

		yield return null;
		Assert.IsFalse (isNewScene);
	}
		
	//DONE
	[UnityTest]
	public IEnumerator CheckActionLogUpdate() {
		yield return null;
		Assert.AreEqual (true, false);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckActionLogSelection() {
		yield return null;
		Assert.AreEqual (true, false);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckPauseMenuSelection() {
		var GameController = new GameController(); 
		bool onPause = GameController.isPaused(true);
		yield return null;
		Assert.IsTrue (onPause);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckUpdateMethodAfterPause() {
		bool updating;
		var GameController = new GameController(); 
		bool onPause = GameController.isPaused(true);
		float t = Time.timeScale;

		if (t == 0 && onPause == true) {
			updating = true;
		} else {
			updating = false;
		}

		yield return null;
		Assert.AreEqual (true, updating);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckUnpauseMenuSelection() {
		var GameController = new GameController(); 
		bool postPause = GameController.isPaused(false);
		yield return null;
		Assert.IsFalse (postPause);
	}

	//Game Time Won't Stop regardless no?
	[UnityTest]
	public IEnumerator CheckGameTimeAfterUnPause() {
		yield return null;
		Assert.AreEqual (true, false);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckUpdateMethodAfterUnPause() {
		bool updating;
		var GameController = new GameController(); 
		bool postPause = GameController.isPaused(false);
		float t = Time.timeScale;

		if (t == 1 && postPause == false) {
			updating = true;
		} else {
			updating = false;
		}
		yield return null;
		Assert.AreEqual (true, updating);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckSettingsChanged() {
		bool setchange = false;
		int newVol = 2;
		var v = new GameController ();
		v.PlayerStatus (newVol);

		if (newVol == AudioListener.volume) {
			setchange = true;
		} else {
		}

		yield return null;
		Assert.AreEqual (true, setchange);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckSavedGame() {
		bool isSaved = false;

		var save = new SaveLoad ();
		save.SaveGame ();

		if (File.Exists (Application.persistentDataPath + "/Savedgame.dat")) {
			isSaved = true;
		} else {
		}

		yield return null;
		Assert.AreEqual (true, isSaved);
	}

	//Don't Need??
	[UnityTest]
	public IEnumerator CheckSavedGameSelection() {
		
		yield return null;
		Assert.AreEqual (true, false);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckLoadGame() {
		bool canLoad = false;

		var loader = new SaveLoad ();
		loader.LoadGame ();
			
		yield return null;
			Assert.AreEqual (true, canLoad);
	}

	//DONE
	[UnityTest]
	public IEnumerator CheckLoadedConditions() {
		bool canLoad = false;
		float health = 9999;
		float speed = 9999;
		float armour = 9999;

		var loader = new SaveLoad ();
		loader.LoadGame ();

		if (health != 9999 && speed != 9999 && armour != 9999) {
			canLoad = true;
		} else {
		}

		yield return null;
		Assert.AreEqual (true, canLoad);
	}

	//What exactly is game status, active??
	[UnityTest]
	public IEnumerator CheckGameStatusUpdateOnMainMenu() {
		yield return null;
		Assert.AreEqual (true, false);
	}

	//Same as above
	[UnityTest]
	public IEnumerator CheckGameStatusUpdateOnPauseMenu() {
		yield return null;
		Assert.AreEqual (true, false);
	}

}

