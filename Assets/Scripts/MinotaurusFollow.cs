using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class MinotaurusFollow : MonoBehaviour {

	public float seeAngle = 180f;

	public bool returnToStartPosition;

	//[SerializeField]
	private GameObject player;	
	
	private NavMeshAgent agent;
	public bool followCharacter;
	private bool seenByCharacter;
	private AudioSource soundTest;

	private Vector3 startPosition;
	

    void Start() {
		player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
		soundTest = GetComponent<AudioSource>();
		startPosition = transform.position;

		seenByCharacter = false;
    }
	
    void Update() {

		float playerAngle = Mathf.Atan2(player.transform.forward.z, player.transform.forward.x); //Winkel der Blickrichtung des Spielers.
		float minotaurAngle = Mathf.Atan2(transform.position.z - player.transform.position.z, transform.position.x - player.transform.position.x); //Winkel des Verbindungsvektors von Golem zu Spieler.
		float angle = (minotaurAngle - playerAngle > 0) ? (minotaurAngle - playerAngle) : (playerAngle - minotaurAngle); //Winkel zwischen Blickrichtung des Spielers und Golem-Spieler-Verbindung.

		angle *= Mathf.Rad2Deg;
		//Debug.Log(angle);

		if(angle > seeAngle/2f && angle < 360f - seeAngle/2f){
			seenByCharacter = false;
		}else{
			seenByCharacter = true;
		}
       
		if(followCharacter){
			if(!seenByCharacter){
				agent.SetDestination(player.transform.position);
			}else{
				agent.SetDestination(transform.position);
			}        	
		}
            
    }
	
	public void inRange(){

		if(soundTest != null && !followCharacter){
			soundTest.Play();
		}
		followCharacter = true;
		Debug.Log("in range");
	}
	
	public void outOfRange(){

		followCharacter = false;

		if(returnToStartPosition){		
			agent.SetDestination(startPosition);
		}

		Debug.Log("out of");
	}
	
}
