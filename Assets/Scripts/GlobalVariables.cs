using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

//	public bool[] switches;
	public bool deleteProgressAtStart; //zum Testen

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
	}

	public void save(){
		//Save switch states.
//		for(int i = 0; i < switches.Length; i++){
//			if(switches[i]){
//				PlayerPrefs.SetInt(Application.loadedLevelName + "Switch" + i, 1);
//			}
//		}
	}

}
