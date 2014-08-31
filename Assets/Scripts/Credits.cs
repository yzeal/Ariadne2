using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	private float time = 20f;

	public GUIStyle title;
	public GUIStyle title2;
	public GUIStyle text;

	private float yPos;

	private bool startMoving;

	private float startTime;

	// Use this for initialization
	void Start () {
		Invoke("Move", 3f);
	}
	
	// Update is called once per frame
	void Update () {

		float t = Time.time - startTime;

		Debug.Log (t/time*100f);

		if(startMoving && yPos < Screen.height*2f){
			yPos = Mathf.Lerp(0f, Screen.height*2f, t/time);
		}

		if(Input.GetButtonDown("Jump")){
			Application.LoadLevel("start");
		}
	}

	private void Move(){
		startMoving = true;
		startTime = Time.time;
	}

	void OnGUI(){
		GUI.Label(new Rect(0f, Screen.height/4f - Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Ariadne", title);

		title2.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(0f, Screen.height/4f + Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Hochschule Trier", title2);
		GUI.Label(new Rect(0f, Screen.height/4f + 2f*Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Game Development SoSe 2014", title2);
		GUI.Label(new Rect(0f, Screen.height/4f + 3f*Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Yasmin Schraven", title2);
		GUI.Label(new Rect(0f, Screen.height/4f + 3.5f*Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Benjamin Sonnenschein", title2);
		GUI.Label(new Rect(0f, Screen.height/4f + 4f*Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Julia Wolf", title2);
		GUI.Label(new Rect(0f, Screen.height/4f + 5f*Screen.height/10f - yPos, Screen.width, Screen.height/10f), "Dozent: Wolfgang Reichert", title2);
		title2.alignment = TextAnchor.MiddleRight;

		GUI.Label(new Rect(0f, Screen.height*3f - Screen.height/2f - yPos, Screen.width, Screen.height/10f), "ende", title);
	}
}
