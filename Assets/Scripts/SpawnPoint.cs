using UnityEngine;
using System.Collections;
using com.ootii.Cameras;

public class SpawnPoint : MonoBehaviour {
	
	public ExitDirection spawnPointDirection;
	public ExitLevel spawnPointLevel;

	
	void Awake () {
		ExitDirection playerDirection = GlobalVariables.Instance.exitDirection;
		ExitLevel playerLevel = GlobalVariables.Instance.exitLevel;

		if(spawnPointDirection == playerDirection && spawnPointLevel == playerLevel){
			GameObject player = GameObject.FindWithTag("Player");
			if(GlobalVariables.Instance.savePoint != Vector3.zero){
				player.transform.position = GlobalVariables.Instance.savePoint;
			}else{
				player.transform.position = transform.position;
			}
			player.transform.rotation = transform.rotation;
			GameObject.FindWithTag("MainCameraRig").transform.position = transform.position - 20f * player.transform.forward;

			if(GlobalVariables.Instance.autoSave) GlobalVariables.Instance.save();
			Debug.Log("Gespawnt: " + spawnPointDirection + ", " + spawnPointLevel);
		}

	}


}
