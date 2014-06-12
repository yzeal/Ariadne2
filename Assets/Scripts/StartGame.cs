using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevel(GlobalVariables.Instance.levelSequence[0]);

		foreach(string l in GlobalVariables.Instance.levelSequence){
			Debug.Log("Level: " + l);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
