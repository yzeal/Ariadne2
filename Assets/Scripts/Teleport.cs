using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;

	public int nextLevel;

	void OnTriggerEnter(Collider other){
		GlobalVariables.Instance.exitDirection = exitDirection;
		GlobalVariables.Instance.exitLevel = exitLevel;

//		GlobalVariables.Instance.currentLevel = nextLevel;
//		if(GlobalVariables.Instance.autoSave) GlobalVariables.Instance.save();
//		Application.LoadLevel(GlobalVariables.Instance.levelSequence[nextLevel]);
//		GlobalVariables.Instance.saveSubtitles();
		GlobalVariables.Instance.changeScene(nextLevel);
	}
}
