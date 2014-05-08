using UnityEngine;
using System.Collections;

public class MinotaurusTrigger : MonoBehaviour {

	private GameObject player;
	public MinotaurusFollow minotaurus;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		
		if(other.CompareTag("Player")){
			minotaurus.inRange();
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			minotaurus.outOfRange();
		}
	}
}
