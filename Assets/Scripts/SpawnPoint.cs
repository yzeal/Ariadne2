using UnityEngine;
using System.Collections;
using com.ootii.Cameras;
//using com.ootii.AI.Controllers;

public class SpawnPoint : MonoBehaviour {
	
	public ExitDirection spawnPointDirection;
	public ExitLevel spawnPointLevel;

	public GameObject mainCameraRig;

	// Use this for initialization
	void Start () {
		ExitDirection playerDirection = GlobalVariables.Instance.exitDirection;
		ExitLevel playerLevel = GlobalVariables.Instance.exitLevel;

		if(spawnPointDirection == playerDirection && spawnPointLevel == playerLevel){
			GameObject player = GameObject.FindWithTag("Player");
//			mainCameraRig.transform.rotation = transform.rotation;
			player.transform.position = transform.position;
			player.transform.rotation = transform.rotation;
			mainCameraRig.transform.position = transform.position - 20f * player.transform.forward;
			Debug.Log("Player positioniert.");
		}

		Debug.Log("Gespawnt.");

	}
	

}
