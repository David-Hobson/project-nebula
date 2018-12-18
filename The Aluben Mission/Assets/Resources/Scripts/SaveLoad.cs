using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
	public float health;
	public float speed;
	public float armour;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SaveGame(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/SavedGame.dat", FileMode.Open);

		PlayerData data = new PlayerData ();
		data.health = health;
		data.speed = speed;
		data.armour = armour;

		bf.Serialize (file, 0);
		file.Close ();
	}

	public void LoadGame(){
		if (File.Exists (Application.persistentDataPath + "/Savedgame.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/SavedGame.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();


			health = data.health;
			speed = data.speed;
			armour = data.armour;
		}
	}

	[System.Serializable]
	class PlayerData{
		public float health;
		public float speed; 
		public float armour;
	}
}

