﻿using UnityEngine;
using System.Collections;

public class Bodenschalter : MonoBehaviour {

	public int id;
	public bool on;

	public GameObject bodenPlatte;
	public GameObject dachPlatte;
	public ParticleSystem partikelOben;
	public ParticleSystem partikelUnten;

	public Color offColor;
	public Color onColor;

	private bool justActivated;
	private Material matBoden;
	private Material matDach;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Switch" + id) == 0){
			on = false;
		}else{
			on = true;
		}

		partikelOben.startColor = onColor;
		partikelUnten.startColor = onColor;

		matDach = dachPlatte.GetComponent<MeshRenderer>().material;
		matBoden = bodenPlatte.GetComponent<MeshRenderer>().material;

		matBoden.SetColor("_Color", offColor);
		matDach.SetColor("_Color", offColor);

		justActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(on){
			matBoden.SetColor("_Color", onColor);
			matDach.SetColor("_Color", onColor);
			partikelOben.Play();
			partikelUnten.Play();
		}else{
			matBoden.SetColor("_Color", offColor);
			matDach.SetColor("_Color", offColor);
			partikelOben.Stop();
			partikelOben.Clear();
			partikelUnten.Stop();
			partikelUnten.Clear();
		}

	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("trigger enter");
		if(!justActivated && !on && other.CompareTag("Player")){
//		if(!on && other.CompareTag("Player")){
			on = true;
			if(GlobalVariables.Instance.autoSave){
				PlayerPrefs.SetInt(Application.loadedLevelName + "Switch" + id, 1);
			}
		}else

		if(!justActivated && on && other.CompareTag("Player")){
//		if(on && other.CompareTag("Player")){
				on = false;
				if(GlobalVariables.Instance.autoSave){
					PlayerPrefs.SetInt(Application.loadedLevelName + "Switch" + id, 0);
				}
			}

		justActivated = true;

	}


	void OnTriggerExit(Collider other){
		justActivated = false;
	}
}
