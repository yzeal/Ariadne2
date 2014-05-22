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
		//Load switch states.
//		for(int i = 0; i < switches.Length; i++){
//			int switchState = PlayerPrefs.GetInt(Application.loadedLevelName + "Switch" + i);
//			if(switchState == 1){
//				switches[i] = true;
//			}
//		}
		exitDirection = (ExitDirection) PlayerPrefs.GetInt("ExitDirection");
		exitLevel = (ExitLevel) PlayerPrefs.GetInt("ExitLevel");
		Debug.Log (exitDirection.ToString() + ", " + exitLevel.ToString());
	}

	public void save(){
		//Save switch states.
//		for(int i = 0; i < switches.Length; i++){
//			if(switches[i]){
//				PlayerPrefs.SetInt(Application.loadedLevelName + "Switch" + i, 1);
//			}
//		}

		PlayerPrefs.SetInt("ExitDirection", (int) exitDirection);
		PlayerPrefs.SetInt("ExitLevel", (int) exitLevel);
		PlayerPrefs.SetString("CurrentLevel", Application.loadedLevelName);
	}

}
