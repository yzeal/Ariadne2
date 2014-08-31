using UnityEngine;
using System.Collections;
using com.ootii.Cameras;

public enum ExitDirection{
	NORTH, EAST, SOUTH, WEST
}

public enum ExitLevel{
	GROUND, AIRWELL
}

public class GlobalVariables : MonoBehaviour {

	public int startLevel;

//	public bool[] switches;
	public bool deleteProgressAtStart; //zum Testen
	public bool autoSave;

	public float minotaurusSpeed = 3.5f;

	public ExitDirection exitDirection;
	public ExitLevel exitLevel;
	public int currentLevel;
	public string[] levelSequence;

	public bool newGame;

	public bool inCrawlArea;
	public bool crawling;
	public float crawlBugFix;

	public Vector3 savePoint = Vector3.zero;

	public static GlobalVariables Instance { get; private set; }

	private DialogHandler dialogHandler;

	void Awake(){
		
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);

		if(deleteProgressAtStart){
			PlayerPrefs.DeleteAll();
			Debug.Log("deleting");
		}
	}
	

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("mainMenu")){
			if(autoSave) save();
			Application.LoadLevel("start");
		}
		//Kamera sofort wieder direkt hinter den Spieler setzen; T-Taste bzw. LB (XBOX 360).
		if(Input.GetButtonDown("resetCamera") && !crawling){
			GameObject camRig = GameObject.FindWithTag("MainCameraRig");
			GameObject player = GameObject.FindWithTag("Player");
			if(camRig != null && player != null){
				camRig.transform.position = player.transform.position + 2f * player.transform.up  - 2f * player.transform.forward;
			}
		}

		//Die Variable wird (aus Gründen?) nicht immer in OnTriggerExit in CrawlArea auf false gesetzt, wenn man diese verlässt, was dazu führt, dass man die Kriechbewegung nicht durch Tastendruck verlassen kann.
		crawlBugFix += Time.deltaTime;
		if(crawlBugFix >= 2f){
			inCrawlArea = false;
			crawlBugFix = 0f;
		}

//		if(Input.GetKeyDown("p")){
//			toggleCrawl = true;
//		}
	}

	void OnGUI(){
		// wg "!dest.m_MultiFrameGUIState.m_NamedKeyControlList"-Error sonst. (???)
	}

//	void OnLevelWasLoaded(){
//		untertitelHandler = GameObject.Find("UntertitelHandler").GetComponent<UntertitelHandler>();
//	}

	void OnLevelWasLoaded(){
		inCrawlArea = false;
		crawlBugFix = 0f;
	}

	public void load(){

		exitDirection = (ExitDirection) PlayerPrefs.GetInt("ExitDirection");
		exitLevel = (ExitLevel) PlayerPrefs.GetInt("ExitLevel");
		currentLevel = PlayerPrefs.GetInt("CurrentLevel");

		if(PlayerPrefs.GetInt("Crawling") != 0){
			crawling = true;
//			toggleCrawl = true;
		}


		for(int i = 0; i < 5; i++){
			levelSequence[i] = PlayerPrefs.GetString("LevelSequence" + i);
		}

		if(levelSequence[0] == ""){
			newGame = true;
			currentLevel = 0;
			for(int i = 0; i < 4; i++){
				int supplevel = Random.Range(0, 1); //TODO Mehr Levelvarianten! TEMP: nur einer
				levelSequence[i] = "level" + i + "-" + supplevel;
			}
			levelSequence[4] = "level4";
			levelSequence[5] = "ende";
		}else{
			minotaurusSpeed = PlayerPrefs.GetFloat("MinotaurusSpeed");
			newGame = false;
		}

		save();
	}

	public void save(){

		PlayerPrefs.SetInt("ExitDirection", (int) exitDirection);
		PlayerPrefs.SetInt("ExitLevel", (int) exitLevel);
		PlayerPrefs.SetInt("CurrentLevel", currentLevel);
		PlayerPrefs.SetFloat("MinotaurusSpeed", minotaurusSpeed);

		if(crawling){
			PlayerPrefs.SetInt("Crawling", 1);
		}

		for(int i = 0; i < 5; i++){
			PlayerPrefs.SetString("LevelSequence" + i, levelSequence[i]);
		}

	}

//	public void saveSubtitles(){
//		if(untertitelHandler != null){
//			for(int i = 0; i < untertitelHandler.subtitles.Length; i++){
//				if(untertitelHandler.subtitles[i] == null){
//					PlayerPrefs.SetInt(Application.loadedLevelName + "Subtitle" + i, 1);
//				}
//			}
//		}
//	}

	public void changeScene(int nLevel){
//		crawling = false;
		Debug.Log("Change Scene: " + nLevel);
		savePoint = Vector3.zero;
		currentLevel = nLevel;
		if(autoSave) PlayerPrefs.SetInt("CurrentLevel", nLevel);
		Application.LoadLevel(levelSequence[nLevel]);
	}

}
