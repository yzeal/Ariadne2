using UnityEngine;
using System.Collections;

public enum ExitDirection{
	NORTH, EAST, SOUTH, WEST
}

public enum ExitLevel{
	GROUND, AIRWELL
}

public class GlobalVariables : MonoBehaviour {

//	public bool[] switches;
	public bool deleteProgressAtStart; //zum Testen
	public bool autoSave;

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;
	public string currentLevel;

	public static GlobalVariables Instance { get; private set; }


	void Awake(){
		
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);

		if(deleteProgressAtStart){
			PlayerPrefs.DeleteAll();
		}else{
			load();
		}
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void load(){

		exitDirection = (ExitDirection) PlayerPrefs.GetInt("ExitDirection");
		exitLevel = (ExitLevel) PlayerPrefs.GetInt("ExitLevel");
		currentLevel = PlayerPrefs.GetString("CurrentLevel");
	}

	public void save(){

		PlayerPrefs.SetInt("ExitDirection", (int) exitDirection);
		PlayerPrefs.SetInt("ExitLevel", (int) exitLevel);
		PlayerPrefs.SetString("CurrentLevel", Application.loadedLevelName);
	}

}
