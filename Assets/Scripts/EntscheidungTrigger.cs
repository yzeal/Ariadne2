using UnityEngine;
using System.Collections;

public class EntscheidungTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			GameObject.Find("Entscheidung").GetComponent<Entscheidung>().enabled = true;
		}
	}
}
