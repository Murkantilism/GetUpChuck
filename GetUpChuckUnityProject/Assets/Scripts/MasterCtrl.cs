using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red chuck
	GameObject RedCK;
	Player RedCKPlayer;
	Inventory RedInv;

	//stored reference to blue chuck
	GameObject BlueCK;
	Player BlueCKPlayer;
	Inventory BlueInv;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	Player activePlayer;
	Inventory activeInventory;

	//camera info
	Camera mainCam;
	public CameraPan cameraPan;

	//tracks position for mouse drags
	Vector2 mouseDragStart;
	Vector2 defMDS = new Vector2 (-1.0f, -1.0f);

	// Use this for initialization
	void Start () {
		BlueCK = GameObject.FindGameObjectWithTag("blueCK");
		RedCK = GameObject.FindGameObjectWithTag("redCK");
		RedCKPlayer = RedCK.GetComponent<Player> ();
		BlueCKPlayer = BlueCK.GetComponent<Player> ();
		RedInv = RedCK.GetComponent<Inventory> ();
		BlueInv = BlueCK.GetComponent<Inventory> ();
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		cameraPan = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraPan>();
		setActivePlayer("red");
		setActivePlayerGO("red");
		setInventory ("red");
		mouseDragStart = defMDS;

	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetButton("CKleft")){
			walkLeft();
		}
		if (Input.GetButton("CKright")){
			walkRight();
		}*/
		if (Input.GetButtonDown("CKjumpL")){
			jumpLeft();
		}
		if (Input.GetButtonDown("CKjumpR")){
			jumpRight();
		}
		if (Input.GetButtonDown("playerSwaps")){
			swapPlayer();
		}

		//TODO Add eat/vomit controls
		//hook

		if (Input.GetMouseButton (0)) {
			if (mouseDragStart == defMDS){
				if (0 < Input.mousePosition.x && Input.mousePosition.x < (mainCam.pixelWidth/3)){
					walkLeft();
				}
				if ((2*mainCam.pixelWidth/3) < Input.mousePosition.x && Input.mousePosition.x < mainCam.pixelWidth){
					walkRight();
				}
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			if ((mainCam.pixelWidth/3) < Input.mousePosition.x && Input.mousePosition.x < (2*mainCam.pixelWidth/3)){
				mouseDragStart = Input.mousePosition;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (mouseDragStart != defMDS){
				if (0 < Input.mousePosition.x && Input.mousePosition.x < (mainCam.pixelWidth/3)){
					jumpLeft();
				}
				if ((2*mainCam.pixelWidth/3) < Input.mousePosition.x && Input.mousePosition.x < mainCam.pixelWidth){
					jumpRight();
				}
			}
			mouseDragStart = defMDS;
		}
		/*foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				if (0 < touch.position.x < (mainCam.pixelWidth/3)){
					walkLeft();
				}
				if ((2*mainCam.pixelWidth/3) < touch.position.x < mainCam.pixelWidth){
					walkRight();
				}
				// Construct a ray from the current touch coordinates
				//var ray = Camera.main.ScreenPointToRay (touch.position);
				//if (Physics.Raycast (ray)) {
				//	// Create a particle if hit
				//	Instantiate (particle, transform.position, transform.rotation);
				//}
			}
		}*/
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

	public void setInventory(string color){
		if(color.Equals("red")){
			activeInventory = RedInv;
		}
		if(color.Equals("blue")){
			activeInventory = BlueInv;
		}
	}

	public Inventory getActiveInventory(){
		return this.activeInventory;
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
		//masterAnimationDel ("walkLeft");
	}

	//walk right
	void walkRight(){
		activePlayer.moveRight();
		//masterAnimationDel ("walkRight");
	}

	//jump left
	void jumpLeft(){
		activePlayer.jump("left");
		masterAnimationDel ("jumpLeft");
	}

	//jump right
	void jumpRight(){
		activePlayer.jump("right");
		masterAnimationDel ("jumpRight");
	}

	//called when player dies
	void playerDeath (){
		activePlayer.playerRe();
		masterAnimationDel ("death");
	}

	void playerEat(Item tmpI){
		activeInventory.AddItem (tmpI);
		masterAnimationDel ("eat");
	}

	void playerUpChuck(Item tmpI){
		activeInventory.DropItem (tmpI);
		masterAnimationDel ("upchuck");
	}

	//called to switch player from red to blue or blue to red
	void swapPlayer(){
		if (activePlayer == RedCKPlayer) {
			setActivePlayer("blue");
			setActivePlayerGO("blue");
			setInventory("blue");
			cameraPan.TriggerPan(BlueCK, RedCK);
		}
		else if (activePlayer == BlueCKPlayer) {
			setActivePlayer("red");
			setActivePlayerGO("red");
			setInventory("red");
			cameraPan.TriggerPan(RedCK, BlueCK);
		}
	}

	//function to delegate animations to animators in specific objects
	void masterAnimationDel (string aniAction){
		//TODO hook up with real animation scrips
		//TODO handle walking animation using x velosity calculation
		//disable if jumping, use y caculation similar to doulbe jump
		if (aniAction.Equals("jumpRight")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.jumpRight();
		}
		if (aniAction.Equals("jumpLeft")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.jumpLeft();
		}
		//if (aniAction.Equals("walkLeft")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.walkLeft();
		//}
		//if (aniAction.Equals("walkRight")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.walkRight();
		//}
		if (aniAction.Equals("death")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.death();
		}
		if (aniAction.Equals("STwalkLeft")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.STwalkLeft();
		}
		if (aniAction.Equals("STwalkRight")){
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.STwalkRight();
		}
		if (aniAction.Equals ("eat")) {
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.eat();
		}
		if (aniAction.Equals ("upchuck")) {
			//myAnimatorPlayer tmpAnimator = activePlayer_go.GetComponent<myAnimatorPlayer>();
			//tmpAnimator.upchuck();
		}

	}

}
