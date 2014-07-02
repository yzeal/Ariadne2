using UnityEngine;
using System.Collections;

public class Hinweis : MonoBehaviour {

	public Bodenschalter[] bodenschalter;

	public Color offColor;
	public Color onColor;

	public MeshRenderer hinweisMesh;

	private bool on;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		on = true;
		foreach(Bodenschalter schalter in bodenschalter){
			if(!schalter.on){
				on = false;
				break;
			}
		}

		if(on){
			hinweisMesh.material.SetColor("_Color", onColor);
		}else{
			hinweisMesh.material.SetColor("_Color", offColor);
		}
	}
}
