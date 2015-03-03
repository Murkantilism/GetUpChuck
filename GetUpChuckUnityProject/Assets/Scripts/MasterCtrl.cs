using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red
	public GameObject Red_GO;
	public Player Red_Player;

	//stored reference to blue
	GameObject Blue_GO;
	Player Blue_Player;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	public Player activePlayer;

	public CameraPan cameraPan;
	CameraZoom cameraZoom;
	CameraZoomOut cameraZoomOut;

	// Use this for initialization
	void Start () {
		Blue_GO = GameObject.Find("bluePlayer");
		Blue_Player = Blue_GO.GetComponent<Player> ();
		Red_GO = GameObject.Find("redPlayer");
		Red_Player = Red_GO.GetComponent<Player> ();
		// Set red to active player first
		setActivePlayer("red");
		setActivePlayerGO("red");
		cameraPan = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPan>();
		cameraZoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoom>();
		cameraZoomOut = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoomOut>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("CKleft")){
			walkLeft();
		}
		if (Input.GetButton("CKright")){
			walkRight();
		}
		if (Input.GetButtonDown("CKjumpL")){
			jumpLeft();
		}
		if (Input.GetButtonDown("CKjumpR")){
			jumpRight();
		}
		if (Input.GetButtonDown("playerSwaps")){
			swapPlayer();
		}
		if (Input.GetKeyDown(KeyCode.Z)){
			openInventory();
		}
		if (Input.GetKeyDown(KeyCode.X)){
			closeInventory();
		}
	}

	public void setActivePlayerGO(string color){
		if(color.Equals("red")){
			activePlayer_go = Red_GO;
		}
		if(color.Equals("blue")){
			activePlayer_go = Blue_GO;
		}
	}

	public GameObject getActivePlayerGO(){
		return this.activePlayer_go;
	}

	public void setActivePlayer(string color){
		if(color.Equals("red")){
			activePlayer = Red_Player;
		}
		if(color.Equals("blue")){
			activePlayer = Blue_Player;
		}
	}

	public Player getActivePlayer(){
		return this.activePlayer;
	}

	//INPUT TAGS
	//CKleft
	//CKright
	//CKjumpL
	//CKjumpR

	//OBJECT TAGS
	//red
	//blue

	//Walks left
	void walkLeft(){
		activePlayer.moveLeft();
	}

	//walk right
	void walkRight(){
		activePlayer.moveRight();
	}

	//jump left
	void jumpLeft(){
		activePlayer.jump("left");
	}

	//jump right
	void jumpRight(){
		activePlayer.jump("right");
	}

	//called when player dies
	void playerDeath (){
		activePlayer.playerRe();
	}

	// Called when player opens inventory
	void openInventory(){
		// TODO: Add a call to open inventory UI
		cameraZoom.TriggerZoom();
	}

	// Called when player exits inventory
	void closeInventory(){
		// TODO: Add a call to close inventory UI
		cameraZoomOut.TriggerZoom();
	}

	//called to switch player from red to blue or blue to red
	void swapPlayer(){
		if (activePlayer == Red_Player) {
			setActivePlayer("blue");
			setActivePlayerGO("blue");
			cameraPan.TriggerPan(Blue_GO, Red_GO);
		}
		else if (activePlayer == Blue_Player) {
			setActivePlayer("red");
			setActivePlayerGO("red");
			cameraPan.TriggerPan(Red_GO, Blue_GO);
		}
	}


}
