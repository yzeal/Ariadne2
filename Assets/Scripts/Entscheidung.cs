using UnityEngine;
using System.Collections;

public class Entscheidung : MonoBehaviour {

	public bool bwaj;

	public GUIStyle menuButton;
	public GUIStyle menuTitle;

	private Texture2D black;
	
	private bool newGame;
	
	private int currentButton;
	
	private bool b1;
	private bool b2;
	
	private bool disable;
	private bool show = true;
	

	void Start () {

		currentButton = 0;

		black = new Texture2D(1,1);
		black.SetPixel(0,0, new Color(0f,0f,0f,0.5f));
		black.Apply();

		enabled = false;

		menuTitle.fontSize = 50;
	}
	

	void Update () {

		GameObject.FindWithTag("Player").GetComponent<com.ootii.AI.Controllers.MotionController>().enabled = false;

		if((Input.GetButtonDown("menuUp") || Input.GetAxis("Vertical") < -0.1f) && !disable){
			currentButton--;
			disable = true;
			if(!IsInvoking("enableMenuScrolling")){
				Invoke("enableMenuScrolling", 0.2f);
			}
			if(currentButton < 0){
				currentButton = 1;
			}
		}else if((Input.GetButtonDown("menuDown") || Input.GetAxis("Vertical") > 0.1f) && !disable){
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
		
		if(show){

			switch(currentButton){
				case 0:  GUI.FocusControl("auswahl1"); break;
				case 1:  GUI.FocusControl("auswahl2"); break;
			}
			
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), black, ScaleMode.StretchToFill);
			
			GUI.Label(new Rect(0f, Screen.height/2f - 200f, Screen.width, 200f), "Gift ins System kippen?", menuTitle);
			
			GUI.SetNextControlName("auswahl1");
			b1 = GUI.Button(new Rect(0f, Screen.height/2f, Screen.width, 100f), "Befehl befolgen", menuButton);

			GUI.SetNextControlName("auswahl2");
			b2 = GUI.Button(new Rect(0f, Screen.height/2f + 100f, Screen.width, 100f), "Befehl widersetzen", menuButton);
			
			
			
			if (Input.GetButton("menuEnter")) {
				switch(currentButton){
				case 0:  b1 = true; break;
				case 1:  b2 = true; break;
				}
			}
			
			if(b1){
				
				Debug.Log("Befehl befolgt.");
				GameObject.FindWithTag("Player").GetComponent<com.ootii.AI.Controllers.MotionController>().enabled = true;
				GameObject.Find("Gas").GetComponent<Gas>().InitializeGas();
				show = false;
				Invoke("GameOver", 5f);
			}
			
			if(b2){
				
				Debug.Log("Befehl widersetzt.");
				GameObject.Find("DialogHandler").GetComponent<DialogHandler>().Activate(1);
				GameObject.FindWithTag("Player").GetComponent<com.ootii.AI.Controllers.MotionController>().enabled = true;
				Destroy(gameObject);
			}
		}
		
	}

	private void GameOver(){
		GlobalVariables.Instance.changeScene(5);
	}


}
