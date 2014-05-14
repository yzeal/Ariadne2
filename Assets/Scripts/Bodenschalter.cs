using UnityEngine;
using System.Collections;

public class Bodenschalter : MonoBehaviour {

	public int id;
	public bool on;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Switch" + id) == 0){
			on = false;
		}else{
			on = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(on){
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
		}
	}

	void OnTriggerEnter(Collider other) {

		if(!on && other.CompareTag("Player")){
			on = true;
			PlayerPrefs.SetInt(Application.loadedLevelName + "Switch" + id, 1);
		}
		
	}
}
