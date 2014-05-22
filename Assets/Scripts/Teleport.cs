using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;

	public string nextLevelName;

	void OnTriggerEnter(Collider other){
		GlobalVariables.Instance.exitDirection = exitDirection;
		GlobalVariables.Instance.exitLevel = exitLevel;
		Application.LoadLevel(nextLevelName);
	}
}
