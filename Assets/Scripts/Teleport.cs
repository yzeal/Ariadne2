using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;

	public int nextLevel;

	void OnTriggerEnter(Collider other){
		GlobalVariables.Instance.exitDirection = exitDirection;
		GlobalVariables.Instance.exitLevel = exitLevel;
		Application.LoadLevel(GlobalVariables.Instance.levelSequence[nextLevel]);
	}
}
