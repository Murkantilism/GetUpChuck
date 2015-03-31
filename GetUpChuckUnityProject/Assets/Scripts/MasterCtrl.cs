﻿using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red
	public GameObject Red_GO;
	public Player Red_Player;
	Inventory_Red RedInv;
	Player_Animator Red_animator;

	//stored reference to blue
	GameObject Blue_GO;
	Player Blue_Player;
	Inventory_Blue BlueInv;
	Player_Animator Blue_animator;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	public Player activePlayer;
	Inventory activeInventory;
	UI_Inventory ui_inventory;
	Player_Animator active_PAnimator;

	//camera info
	Camera mainCam;
	public CameraPan cameraPan;
	CameraZoom cameraZoom;
	CameraZoomOut cameraZoomOut;

	//item pickup ranges
	public float XEatTol;
	public float YEatTol;

	Player_Animator playerAnimator;

	// Input variables
	public float startTime;
	public Vector2 startPos;
	public bool couldBeSwipe;
	public bool chargingJump;
	public float comfortZone = 10;
	public float minSwipeDist;
	public float maxSwipeTime;

	// Use this for initialization
	void Start () {

		ui_inventory = GameObject.Find("InventoryPanel").GetComponent<UI_Inventory>();

		Blue_GO = GameObject.Find("bluePlayer");
		Blue_Player = Blue_GO.GetComponent<Player> ();
		Red_GO = GameObject.Find("redPlayer");
		Red_Player = Red_GO.GetComponent<Player> ();

		RedInv = Red_GO.GetComponent<Inventory_Red> ();
		BlueInv = Blue_GO.GetComponent<Inventory_Blue> ();

		Red_animator = Red_GO.GetComponent<Player_Animator> ();
		Blue_animator = Blue_GO.GetComponent<Player_Animator> ();

		// Set red to active player first
		setActivePlayer("red");
		setActivePlayerGO("red");
		setInventory ("red");
		setActivePAni ("red");

		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		cameraPan = mainCam.GetComponent<CameraPan>();
		cameraZoom = mainCam.GetComponent<CameraZoom>();
		cameraZoomOut = mainCam.GetComponent<CameraZoomOut>();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButton(0)) {
			// Simulate touch events from mouse events
			if (Input.touchCount == 0) {
				if (Input.GetMouseButtonDown(0) ) {
					HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
				}
				if (Input.GetMouseButton(0) ) {
					HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
				}

			}
		}else if (Input.GetMouseButtonUp(0) ) {
			HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
		}
		#endif


		#if UNITY_IPHONE || UNITY_ANDROID
		if(Input.touchCount > 0){
			HandleTouch(Input.GetTouch(0).fingerId, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Input.GetTouch(0).phase);
		}
		#endif
	}

	private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase) {
		switch(touchPhase){
		case TouchPhase.Began:
			couldBeSwipe = false;
			chargingJump = false;
			startPos = touchPosition;
			startTime = Time.time;
			break;
			
		case TouchPhase.Moved:
			// Handle movement
			if(touchPosition.x >= activePlayer.transform.position.x){
				activePlayer.moveRight();
			}else if(touchPosition.x < activePlayer.transform.position.x){
				activePlayer.moveLeft();
			}
			if(Mathf.Abs(touchPosition.y - startPos.y) < comfortZone){
				couldBeSwipe = false;
			}else{
				couldBeSwipe = true;
			}

			// Calculate the current swipe's direction while moving
			float curSwipeDirection = Mathf.Sign(touchPosition.y - startPos.y);

			if(curSwipeDirection == -1 && couldBeSwipe){
				chargingJump = true;
				activePlayer.chargeJump();
			}else{
				chargingJump = false;
			}

			break;
			
		case TouchPhase.Stationary:
			couldBeSwipe = false;

			// Handle movement
			Debug.Log (touchPosition.x);
			if(touchPosition.x >= activePlayer.transform.position.x){
				activePlayer.moveRight();
			}else if(touchPosition.x < activePlayer.transform.position.x){
				activePlayer.moveLeft();
			}
			break;
			
		case TouchPhase.Ended:
			float swipeDirection = Mathf.Sign(touchPosition.y - startPos.y);
			Vector2 swipeVector = Camera.main.ScreenToWorldPoint(touchPosition) - Camera.main.ScreenToWorldPoint(startPos);
			Debug.Log("SWIPE VECTOR" + swipeVector);

			// Based on swipe direction, jump
			if(touchPosition.x >= activePlayer.transform.position.x && swipeDirection == -1 && couldBeSwipe && chargingJump){
				activePlayer.triggerJump("right", swipeVector);
				chargingJump = false;
			}else if(touchPosition.x < activePlayer.transform.position.x && swipeDirection == -1 && couldBeSwipe && chargingJump){
				activePlayer.triggerJump("left", swipeVector);
				chargingJump = false;
			}
			break;
		}
	}

	//===========================================================
	//getters and setters

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

	public void setActivePAni(string color){
		if(color.Equals("red")){
			active_PAnimator = Red_animator;
		}
		if (color.Equals ("blue")) {
			active_PAnimator = Blue_animator;
		}
	}



	//======================================================================

	//Walks left
	void walkLeft(){
		activePlayer.moveLeft();
		masterAnimationDel ("walkLeft");
	}

	//walk right
	void walkRight(){
		activePlayer.moveRight();
		masterAnimationDel ("walkRight");
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
		activePlayer.changeSize (tmpI.weight);
		masterAnimationDel ("eat");
	}

	void playerUpChuck(Item tmpI){
		activeInventory.DropItem (tmpI);
		masterAnimationDel ("upchuck");
	}

	void playerIdle(){
		masterAnimationDel("idle");
	}

	// Called when player opens inventory
	void openInventory(){
		cameraZoom.SetZoomState(true);
		Debug.Log("Master call to Open Inventory");
		ui_inventory.OpenInventory(activeInventory);
	}

	// Called when player exits inventory
	void closeInventory(){
		cameraZoomOut.SetZoomState(true);
		ui_inventory.CloseInventory(activeInventory);
	}

	//called to switch player from red to blue or blue to red
	public void swapPlayer(){
		if (activePlayer == Red_Player) {
			setActivePlayer("blue");
			setActivePlayerGO("blue");

			cameraPan.TriggerPan(Blue_GO, Red_GO);
			setInventory("blue");
		}
		else if (activePlayer == Blue_Player) {
			setActivePlayer("red");
			setActivePlayerGO("red");
			cameraPan.TriggerPan(Red_GO, Blue_GO);
			setInventory("red");
		}
	}

	//function to delegate animations to animators in specific objects
	void masterAnimationDel (string aniAction){
		//TODO handle walking animation using x velosity calculation, disable if jumping, use y caculation similar to doulbe jump
		if (aniAction.Equals("jumpRight")){
			active_PAnimator.Set_animation_state(2);
		}
		if (aniAction.Equals("jumpLeft")){
			active_PAnimator.Set_animation_state(2);
			//TODO Faceing
		}
		// TODO: hook up death animation
		if (aniAction.Equals("death")){
			//active_PAnimator.Set_animation_state(???);
		}
		if (aniAction.Equals("STwalkLeft")){
			active_PAnimator.Set_animation_state(1);
			//TODO faceing
		}
		if (aniAction.Equals("STwalkRight")){
			active_PAnimator.Set_animation_state(1);
		}
		if (aniAction.Equals ("eat")) {
			active_PAnimator.Set_animation_state(3);
		}
		if (aniAction.Equals ("upchuck")) {
			active_PAnimator.Set_animation_state(4);
		}
	}

}
