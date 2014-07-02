using UnityEngine;
using System.Collections;

public class MinotaurusTrigger : MonoBehaviour {

	private GameObject player;
	public MinotaurusFollow minotaurus;

	public float range;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float d = Vector3.Distance(player.transform.position, transform.position);
		if(d > range){
			minotaurus.outOfRange();
		}
	}

	void OnTriggerEnter(Collider other){
		
		if(other.CompareTag("Player")){
			minotaurus.inRange();
		}
	}
	
//	void OnTriggerExit(Collider other){
//		if(other.CompareTag("Player")){
//			minotaurus.outOfRange();
//		}
//	}
}
