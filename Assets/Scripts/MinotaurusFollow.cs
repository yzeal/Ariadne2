﻿using UnityEngine;
using System.Collections;
using com.ootii.AI.Controllers;

[RequireComponent (typeof (AudioSource))]
public class MinotaurusFollow : MonoBehaviour {

	public float seeAngle = 180f;

	public bool returnToStartPosition;

	//[SerializeField]
	private GameObject player;	
	
	private NavMeshAgent agent;
	public bool followCharacter;
	private bool seenByCharacter;
	public AudioSource soundTest;

	private bool stop;

	public AudioSource grargh;

	private Vector3 startPosition;

	private bool glowing;
//	private Texture2D schwarz;
	public Texture schwarz;
	private Texture glowy;
	private Material mat;

    void Start() {
		player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
//		soundTest = GetComponent<AudioSource>();
		startPosition = transform.position;

		seenByCharacter = false;

		agent.speed = GlobalVariables.Instance.minotaurusSpeed;

		mat = GetComponentInChildren<MeshRenderer>().material;

		glowy = mat.GetTexture("_Illum");

		mat.SetTexture("_Illum", schwarz);
    }
	
    void Update() {

		float playerAngle = Mathf.Atan2(player.transform.forward.z, player.transform.forward.x); //Winkel der Blickrichtung des Spielers.
		float minotaurAngle = Mathf.Atan2(transform.position.z - player.transform.position.z, transform.position.x - player.transform.position.x); //Winkel des Verbindungsvektors von Golem zu Spieler.
		float angle = (minotaurAngle - playerAngle > 0) ? (minotaurAngle - playerAngle) : (playerAngle - minotaurAngle); //Winkel zwischen Blickrichtung des Spielers und Golem-Spieler-Verbindung.

		angle *= Mathf.Rad2Deg;
		//Debug.Log(angle);

		if(angle > seeAngle/2f && angle < 360f - seeAngle/2f){
			seenByCharacter = false;
		}else{
			seenByCharacter = true;
		}
       
		if(followCharacter){
			if(!seenByCharacter){
				if(!soundTest.isPlaying){
					soundTest.Play();
				}
				agent.SetDestination(player.transform.position);
				if(!glowing){
					glowing = true;
					mat.SetTexture("_Illum", glowy);
				}
			}else{
				if(soundTest.isPlaying){
					soundTest.Pause();
				}
				agent.SetDestination(transform.position);
				if(glowing){
					glowing = false;
					mat.SetTexture("_Illum", schwarz);
				}
			}        	
		}

		if(stop){
			agent.SetDestination(transform.position);
			if(glowing){
				glowing = false;
//				mat.SetTexture("_Illum", schwarz);
			}
		}

//		if(!glowing && followCharacter){
//			glowing = true;
//			mat.SetTexture("_Illum", glowy);
//		}else if(glowing && !followCharacter){
//			glowing = false;
//			mat.SetTexture("_Illum", schwarz);
//		}
            
    }

	void OnCollisionEnter(Collision other){
		if(other.gameObject.CompareTag("Player")){
			grargh.Play();
			if(!IsInvoking("kill")){
				if(soundTest != null && soundTest.isPlaying){
					soundTest.Stop();
				}
				stop = true;
				other.gameObject.GetComponent<MotionController>().enabled = false;
				Invoke("kill", grargh.clip.length);
			}
		}
	}

	private void kill(){
		Application.LoadLevel(GlobalVariables.Instance.levelSequence[GlobalVariables.Instance.currentLevel]);
	}
	
	public void inRange(){

		if(soundTest != null && !soundTest.isPlaying){
			soundTest.Play();
		}
		followCharacter = true;
		Debug.Log("in range");
	}
	
	public void outOfRange(){

		if(soundTest != null && soundTest.isPlaying){
			soundTest.Stop();
		}

		followCharacter = false;

		if(returnToStartPosition){		
			agent.SetDestination(startPosition);
		}

//		Debug.Log("out of");
	}

}
