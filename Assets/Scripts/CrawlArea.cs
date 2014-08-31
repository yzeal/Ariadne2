﻿using UnityEngine;
using System.Collections;

public class CrawlArea : MonoBehaviour {

	public bool repos;

	public bool justActivated;

	private GameObject player;
	private GameObject camRig;

	void Start(){
		player = GameObject.FindWithTag("Player");
		camRig = GameObject.FindWithTag("MainCameraRig");
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player") && !justActivated){
			GlobalVariables.Instance.inCrawlArea = true;
			justActivated = true;
			GlobalVariables.Instance.crawling = true;

			if(repos) camRig.transform.position = player.transform.position - 1.2f * player.transform.forward;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			GlobalVariables.Instance.crawling = false;
			GlobalVariables.Instance.inCrawlArea = false;
			justActivated = false;
//			GlobalVariables.Instance.crawling = false;
		}
	}

//	void OnTriggerStay(Collider other){
//		if(other.CompareTag("Player")){
//			camRig.transform.position = player.transform.position - 1.2f * player.transform.forward;
////			camRig.transform.position = player.transform.position;
//		}
//	}

	void OnTriggerStay(Collider other){
		if(other.CompareTag("Player")){
			GlobalVariables.Instance.crawling = true;
			GlobalVariables.Instance.inCrawlArea = true;
		}
	}



}
