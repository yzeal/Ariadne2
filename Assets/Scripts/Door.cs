using UnityEngine;
using System.Collections;

public enum DoorDirection{
	UP, LEFT, RIGHT, DOWN, FWD, BWD
}

public class Door : MonoBehaviour {

	public int id;
	public DoorDirection openDirection;
	public int openDistance = 1;

	public Bodenschalter[] bodenschalter;
	public bool open;

	private Vector3 endpos;//TEMP
	private Vector3 startpos;

	public Color offColor;
	public Color onColor;
	
	private Material mat;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Door" + id) == 0){
			open = false;
		}else{
			open = true;
		}

		mat = GetComponent<MeshRenderer>().material;

		if(bodenschalter.Length == 1){
			onColor = bodenschalter[0].onColor;
			offColor = bodenschalter[0].offColor;
		}

		if(open){
			mat.SetColor("_Color", onColor);
		}else{
			mat.SetColor("_Color", offColor);
		}

		switch(openDirection){
//			case DoorDirection.UP: endpos = transform.position + Vector3.up*transform.localScale.y; break;
//			case DoorDirection.DOWN: endpos = transform.position + Vector3.down*transform.localScale.y; break;
//			case DoorDirection.LEFT: endpos = transform.position + Vector3.left*transform.localScale.y; break;
//			default: endpos = transform.position + Vector3.right*transform.localScale.y; break;
			case DoorDirection.UP: endpos = transform.position + transform.up*openDistance*5.5f; break;
			case DoorDirection.DOWN: endpos = transform.position - transform.up*openDistance*5.5f; break;
			case DoorDirection.LEFT: endpos = transform.position - transform.right*openDistance*5.5f; break;
			case DoorDirection.FWD: endpos = transform.position + transform.forward*openDistance*5.5f; break;
			case DoorDirection.BWD: endpos = transform.position - transform.forward*openDistance*5.5f; break;
			default: endpos = transform.position + transform.right*openDistance*5.5f; break;
		}

		startpos = transform.position;
//		endpos = transform.position + Vector3.down*transform.localScale.y;//TEMP
	
	}
	
	// Update is called once per frame
	void Update () {
		open = true;
		foreach(Bodenschalter schalter in bodenschalter){
			if(!schalter.on){
				open = false;
				break;
			}
		}

		if(open && transform.position != endpos){
			if(GlobalVariables.Instance != null && GlobalVariables.Instance.autoSave){
				PlayerPrefs.SetInt(Application.loadedLevelName + "Door" + id, 1);
			}
			transform.position = Vector3.Lerp(transform.position, endpos, Time.deltaTime);//TEMP
//			Debug.Log("Tür " + id + " geöffnet.");
		}

		if(!open && transform.position != startpos){
			if(GlobalVariables.Instance != null && GlobalVariables.Instance.autoSave){
				PlayerPrefs.SetInt(Application.loadedLevelName + "Door" + id, 0);
			}
			transform.position = Vector3.Lerp(transform.position, startpos, Time.deltaTime);//TEMP
//			Debug.Log("Tür " + id + " geschlossen.");
		}

		if(open){
			mat.SetColor("_Color", onColor);
		}else{
			mat.SetColor("_Color", offColor);
		}

	}
}
