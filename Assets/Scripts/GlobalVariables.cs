using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public bool[] switches;

	public static GlobalVariables Instance { get; private set; }
	
	void Awake(){
		
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void load(){
		//Load switch states.
		for(int i = 0; i < switches.Length; i++){
			int switchState = PlayerPrefs.GetInt("Switch"+i);
			if(switchState == 1){
				switches[i] = true;
			}
		}
	}

	public void save(){
		//Save switch states.
		for(int i = 0; i < switches.Length; i++){
			if(switches[i]){
				PlayerPrefs.SetInt("Switch"+i, 1);
			}
		}
	}
}
