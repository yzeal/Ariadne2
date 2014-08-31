using UnityEngine;
using System.Collections;
using com.ootii.AI.Controllers;

public class Teleport : MonoBehaviour {

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;

	public int nextLevel;

	private bool justActivated;

	private bool blend;	
	private Texture2D schwarz;
	private float alpha = 0f;

	void Start () {
		schwarz = new Texture2D(1,1);
		schwarz.SetPixel(0,0, new Color(0f,0f,0f));
		schwarz.Apply();
	}

	void Update () {
		if(blend){
			if(alpha <= 1f){
				alpha += Time.deltaTime;
				if(alpha > 1f) alpha = 1f;
			}
		}
	}
	
	void OnGUI(){
		if(blend){
			GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b, alpha);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), schwarz, ScaleMode.StretchToFill);
		}
	}

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player") && !justActivated){

			justActivated = true;
			blend = true;
			GameObject.FindWithTag("Player").GetComponent<MotionController>().enabled = false;

			Debug.Log("Teleport " + exitDirection + ", " + exitLevel);
			GlobalVariables.Instance.exitDirection = exitDirection;
			GlobalVariables.Instance.exitLevel = exitLevel;


//			GlobalVariables.Instance.changeScene(nextLevel);
			Invoke("changeScene", 1.5f);
		}
	}

	private void changeScene(){
		GlobalVariables.Instance.changeScene(nextLevel);
	}
}
