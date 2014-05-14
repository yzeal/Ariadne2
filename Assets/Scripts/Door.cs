using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int id;

	public Bodenschalter[] bodenschalter;
	public bool open;

	private Vector3 endpos;//TEMP

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Door" + id) == 0){
			open = false;
		}else{
			open = true;
		}

		endpos = transform.position + Vector3.down*transform.localScale.y;//TEMP
	
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

		if(open){
			PlayerPrefs.SetInt(Application.loadedLevelName + "Door" + id, 1);
			transform.position = Vector3.Lerp(transform.position, endpos, Time.deltaTime);//TEMP
			Debug.Log("Tür " + id + " geöffnet.");
		}

	}
}
