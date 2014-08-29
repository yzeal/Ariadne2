using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GUIStyle menuButton;
	public GUIStyle menuTitle;

	private Texture2D black;

	private bool newGame;

	private int currentButton;

	private bool b1;
	private bool b2;

	private bool disable;

	// Use this for initialization
	void Start () {

		GlobalVariables.Instance.load();

		newGame = GlobalVariables.Instance.newGame;

		if(newGame){
			currentButton = 0;
		}else{
			currentButton = 1;
		}

		black = new Texture2D(1,1);
		black.SetPixel(0,0, new Color(0f,0f,0f));
		black.Apply();

	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButtonDown("menuUp") || Input.GetAxis("Vertical") < -0.1f) && !newGame && !disable){
			currentButton--;
			disable = true;
			if(!IsInvoking("enableMenuScrolling")){
				Invoke("enableMenuScrolling", 0.2f);
			}
			if(currentButton < 0){
				currentButton = 1;
			}
		}else if((Input.GetButtonDown("menuDown") || Input.GetAxis("Vertical") > 0.1f) && !newGame && !disable){
			currentButton++;
			disable = true;
			if(!IsInvoking("enableMenuScrolling")){
				Invoke("enableMenuScrolling", 0.2f);
			}
			if(currentButton > 1){
				currentButton = 0;
			}
		}
		Debug.Log(Input.GetAxis("Vertical"));

	}

	private void enableMenuScrolling(){
		disable = false;
	}

	void OnGUI(){


		switch(currentButton){
		case 0:  GUI.FocusControl("neues spiel"); break;
		case 1:  GUI.FocusControl("fortsetzen"); break;
		}

		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), black, ScaleMode.StretchToFill);

		GUI.Label(new Rect(0f, Screen.height/2f - 200f, Screen.width, 200f), "Ariadne", menuTitle);

		GUI.SetNextControlName("neues spiel");
		b1 = GUI.Button(new Rect(0f, Screen.height/2f, Screen.width, 100f), "neues spiel", menuButton);
		if(newGame){
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 0.5f);
			GUI.enabled = false;
		}
		GUI.SetNextControlName("fortsetzen");
		b2 = GUI.Button(new Rect(0f, Screen.height/2f + 100f, Screen.width, 100f), "fortsetzen", menuButton);



		if (Input.GetButton("menuEnter")) {
			switch(currentButton){
			case 0:  b1 = true; break;
			case 1:  b2 = true; break;
			}
		}

		if(b1){

			PlayerPrefs.DeleteAll();

			GlobalVariables.Instance.load();

			Application.LoadLevel(GlobalVariables.Instance.levelSequence[4]);
			
			foreach(string l in GlobalVariables.Instance.levelSequence){
				Debug.Log("Level: " + l);
			}
		}





		if(b2){
			Debug.Log("currentLevel: " + GlobalVariables.Instance.currentLevel);
			Application.LoadLevel(GlobalVariables.Instance.levelSequence[GlobalVariables.Instance.currentLevel]);
			
			foreach(string l in GlobalVariables.Instance.levelSequence){
				Debug.Log("Level: " + l);
			}
		}

		if(newGame){
			GUI.enabled = true;
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
		}



	}
}
