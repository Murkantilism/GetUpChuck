using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red chuck
	GameObject RedCK;
	Player RedCKPlayer;

	//stored reference to blue chuck
	GameObject BlueCK;
	Player BlueCKPlayer;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	Player activePlayer;

	public CameraPan cameraPan;
	CameraZoom cameraZoom;
	CameraZoomOut cameraZoomOut;

	// Use this for initialization
	void Start () {
		BlueCK = GameObject.FindGameObjectWithTag("blueCK");
		RedCK = GameObject.FindGameObjectWithTag("redCK");
		RedCKPlayer = RedCK.GetComponent<Player> ();
		BlueCKPlayer = BlueCK.GetComponent<Player> ();
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
			activePlayer_go = RedCK;
		}
		if(color.Equals("blue")){
			activePlayer_go = BlueCK;
		}
	}

	public GameObject getActivePlayerGO(){
		return this.activePlayer_go;
	}

	public void setActivePlayer(string color){
		if(color.Equals("red")){
			activePlayer = RedCKPlayer;
		}
		if(color.Equals("blue")){
			activePlayer = BlueCKPlayer;
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
	//redCK
	//blueCK

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
		if (activePlayer == RedCKPlayer) {
			setActivePlayer("blue");
			setActivePlayerGO("blue");
			cameraPan.TriggerPan(BlueCK, RedCK);
		}
		else if (activePlayer == BlueCKPlayer) {
			setActivePlayer("red");
			setActivePlayerGO("red");
			cameraPan.TriggerPan(RedCK, BlueCK);
		}
	}


}
