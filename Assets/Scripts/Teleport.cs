using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;

	public int nextLevel;

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player")){

			Debug.Log("Teleport " + exitDirection + ", " + exitLevel);
			GlobalVariables.Instance.exitDirection = exitDirection;
			GlobalVariables.Instance.exitLevel = exitLevel;


			GlobalVariables.Instance.changeScene(nextLevel);
		}
	}
}
