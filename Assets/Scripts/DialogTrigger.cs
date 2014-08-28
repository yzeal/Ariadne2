using UnityEngine;
using System.Collections;

public class DialogTrigger : MonoBehaviour {

	public int number;
	private DialogHandler handler;

	// Use this for initialization
	void Start () {
		handler = GameObject.Find("DialogHandler").GetComponent<DialogHandler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			handler.Activate(number);
		}
	}
}
