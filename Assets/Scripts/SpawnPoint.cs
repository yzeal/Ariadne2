using UnityEngine;
using System.Collections;
using com.ootii.Cameras;
//using com.ootii.AI.Controllers;

public class SpawnPoint : MonoBehaviour {

	public int currentLevelNumber;
	public ExitDirection spawnPointDirection;
	public ExitLevel spawnPointLevel;

//	public GameObject mainCameraRig;

	// Use this for initialization
	void Start () {
		ExitDirection playerDirection = GlobalVariables.Instance.exitDirection;
		ExitLevel playerLevel = GlobalVariables.Instance.exitLevel;

		if(spawnPointDirection == playerDirection && spawnPointLevel == playerLevel){
			GameObject player = GameObject.FindWithTag("Player");
//			mainCameraRig.transform.rotation = transform.rotation;
			player.transform.position = transform.position;
			player.transform.rotation = transform.rotation;
//			GameObject mainCameraRig = GameObject.FindWithTag("MainCameraRig");
//			mainCameraRig.transform.position = transform.position - 20f * player.transform.forward;
			GameObject.FindWithTag("MainCameraRig").transform.position = transform.position - 20f * player.transform.forward;

//			GlobalVariables.Instance.currentLevel = currentLevelNumber;
			if(GlobalVariables.Instance.autoSave) GlobalVariables.Instance.save();
			Debug.Log("Gespawnt: " + spawnPointDirection + ", " + spawnPointLevel);
		}

	}
	

}
