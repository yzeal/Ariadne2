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
	public int currentLevel;
	public string[] levelSequence;

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
			//TODO neue levelSequence
		}//else{
//			load();
//		}
		load();
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	public void load(){

		exitDirection = (ExitDirection) PlayerPrefs.GetInt("ExitDirection");
		exitLevel = (ExitLevel) PlayerPrefs.GetInt("ExitLevel");
		currentLevel = PlayerPrefs.GetInt("CurrentLevel");


		for(int i = 0; i < 5; i++){
			levelSequence[i] = PlayerPrefs.GetString("LevelSequence" + i);
		}

		if(levelSequence[0] == ""){
			for(int i = 0; i < 4; i++){
				int supplevel = Random.Range(0, 2); //TODO mehr als 2 sublevel!
				levelSequence[i] = "level" + i + "-" + supplevel;
			}
			levelSequence[4] = "level4";
		}

	}

	public void save(){

		PlayerPrefs.SetInt("ExitDirection", (int) exitDirection);
		PlayerPrefs.SetInt("ExitLevel", (int) exitLevel);
		PlayerPrefs.SetInt("CurrentLevel", currentLevel);

		for(int i = 0; i < 5; i++){
			PlayerPrefs.SetString("LevelSequence" + i, levelSequence[i]);
		}
	}

}
