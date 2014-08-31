﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Dialog : MonoBehaviour {

	public int id;

	public AudioSource audio;

	public int[] times; //in Sekunden
	public string[] texts;
	public int[] styles;
	//(Kein Dictionary, da der sich nicht einfach im Editor bearbeiten lässt. Eine Klasse schien dafür etwas viel Overhead.)

	public bool active;

//	public GUIStyle textStyle;
	public GUIStyle[] textStyles;

	private int currentText = 0;
	private Texture2D schwarz;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Subtitle" + id) != 0){
			Destroy(gameObject);
		}

		if(times.Length != texts.Length){
			Debug.Log("Untertitel: Anzahl Zeiten passt nicht zur Anzahl Texte.");
			Destroy(gameObject);
		}

		audio = GetComponent<AudioSource>();

		if(audio.clip == null){
			Debug.Log("Untertitel: Kein AudioClip vorhanden.");
			Destroy(gameObject);
		}

		schwarz = new Texture2D(1,1);
		schwarz.SetPixel(0,0, new Color(0f,0f,0f,0.5f));
		schwarz.Apply();
	}

	//TESTI
	void Update(){
		if(Input.GetKeyDown("m")){
			GameObject.Find("Entscheidung").GetComponent<Entscheidung>().enabled = true;
		}

	}

	void OnGUI(){
		if(active){
			if(audio.isPlaying){
				if(currentText < times.Length - 1 && audio.time > times[currentText+1]){
					currentText++;
				}
//				GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height*0.1f), schwarz, ScaleMode.StretchToFill);
//				GUI.Label(new Rect(Screen.width*0.05f, 0f, Screen.width*0.9f, Screen.height*0.1f), texts[currentText], textStyle);
				GUIStyle textStyle = textStyles[styles[currentText]];
				Debug.Log(textStyle.normal.textColor);
				GUI.DrawTexture(new Rect(0f, Screen.height*0.9f, Screen.width, Screen.height*0.1f), schwarz, ScaleMode.StretchToFill);
				GUI.Label(new Rect(Screen.width*0.05f, Screen.height*0.9f, Screen.width*0.9f, Screen.height*0.1f), texts[currentText], textStyle);
			}else{
				Debug.Log("Untertitel: Vorbei.");
//				if(GlobalVariables.Instance.autoSave)
					PlayerPrefs.SetInt(Application.loadedLevelName + "Subtitle" + id, 1);
				Destroy(gameObject);
			}
		}
	}

	public void Activate(){
		if(!active){
			active = true;
			audio.Play();
		}
	}

	public void DeactivatePause(){
		if(active){
			active = false;
			audio.Pause();
		}
	}

	public void DeactivateReset(){
		if(active){
			active = false;
			audio.Stop();
			audio.time = 0f;
			currentText = 0;
		}
	}
}
