﻿using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red
	public GameObject Red_GO;
	public Player Red_Player;
	Inventory_Red RedInv;
	Animator Red_animator;
	Animator jelly_Red_animator;

	//stored reference to blue
	public GameObject Blue_GO;
	Player Blue_Player;
	Inventory_Blue BlueInv;
	Animator Blue_animator;
	Animator jelly_Blue_animator;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	public Player activePlayer;
	Inventory activeInventory;
	UI_Inventory ui_inventory;
	public Animator active_PAnimator;

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

	Color sickColor = new Color(0.502f, 0.392f, 0.118f, 1.0f);
	Color healthyColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	// Use this for initialization
	void Start () {

		ui_inventory = GameObject.Find("InventoryPanel").GetComponent<UI_Inventory>();

		Blue_GO = GameObject.Find("bluePlayer");
		Blue_Player = Blue_GO.GetComponent<Player> ();
		Red_GO = GameObject.Find("redPlayer");
		Red_Player = Red_GO.GetComponent<Player> ();

		RedInv = Red_GO.GetComponent<Inventory_Red> ();
		BlueInv = Blue_GO.GetComponent<Inventory_Blue> ();

		Red_animator = Red_GO.GetComponent<Animator> ();
		Blue_animator = Blue_GO.GetComponent<Animator> ();

		jelly_Red_animator = GameObject.Find("redPlayer_j").GetComponent<Animator>();
		jelly_Blue_animator = GameObject.Find("bluePlayer_j").GetComponent<Animator>();

		// Set red to active player first
		setActivePlayer("red");
		setActivePlayerGO("red");
		setInventory ("red");
		setActivePAni ("red");

		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		cameraPan = mainCam.GetComponent<CameraPan>();
		cameraZoom = mainCam.GetComponent<CameraZoom>();
		cameraZoomOut = mainCam.GetComponent<CameraZoomOut>();

		Blue_GO.GetComponent<UnityJellySprite>().renderer.material.color = new Color(35.0f/255.0f, 92.0f/255.0f, 205.0f/255.0f, 1.0f);//Color.blue;
		Red_GO.GetComponent<UnityJellySprite>().renderer.material.color = new Color(200.0f/255.0f, 35.0f/255.0f, 35.0f/255.0f, 1.0f);//Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButton(0)) {
			// Simulate touch events from mouse events
			if (Input.touchCount == 0) {
				if (Input.GetMouseButtonDown(0) ) {
					HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition, TouchPhase.Began);
				}
				if (Input.GetMouseButton(0) ) {
					HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition, TouchPhase.Moved);
				}

			}
		}else if (Input.GetMouseButtonUp(0) ) {
			HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition, TouchPhase.Ended);
		}else{
			playerIdle();
		}
		#endif


		#if UNITY_IPHONE || UNITY_ANDROID
		if(Input.touchCount > 0){
			HandleTouch(Input.GetTouch(0).fingerId, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Input.GetTouch(0).position, Input.GetTouch(0).phase);
		}
		#endif
	}

	private void HandleTouch(int touchFingerId, Vector2 touchPositionWorldPoint, Vector3 realPosition, TouchPhase touchPhase) {
		switch(touchPhase){
		case TouchPhase.Began:
			couldBeSwipe = false;
			chargingJump = false;
			startPos = touchPositionWorldPoint;
			startTime = Time.time;
			break;
			
		case TouchPhase.Moved:
			// Handle movement
			if(touchPositionWorldPoint.x >= activePlayer.transform.position.x){
				walkRight();
			}else if(touchPositionWorldPoint.x < activePlayer.transform.position.x){
				walkLeft();
			}
			if(Mathf.Abs(touchPositionWorldPoint.y - startPos.y) < comfortZone){
				couldBeSwipe = false;
			}else{
				couldBeSwipe = true;
			}

			// Calculate the current swipe's direction while moving
			float curSwipeDirection = Mathf.Sign(touchPositionWorldPoint.y - startPos.y);

			if(couldBeSwipe){
				// Cast a ray to check if the input is over the player
				Ray ray = Camera.main.ScreenPointToRay(realPosition);
				RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
				// If the raycast hit exists and the jump isn't already being charged and it could be a swipe
				if(hit && chargingJump == false){	
					// Get the hit's collider, check if it's the same layer as the players (Jelly)
					if(hit.collider.gameObject.layer == LayerMask.NameToLayer("JellySprites")){
						chargingJump = true;
					}
				}
			}
			if(chargingJump){
				activePlayer.chargeJump(true);
			}
			break;
			
		case TouchPhase.Stationary:
			couldBeSwipe = false;

			// Handle movement
			Debug.Log (touchPositionWorldPoint.x);
			if(touchPositionWorldPoint.x >= activePlayer.transform.position.x){
				walkRight();
			}else if(touchPositionWorldPoint.x < activePlayer.transform.position.x){
				walkLeft();
			}
			break;
			
		case TouchPhase.Ended:
			float swipeDirection = Mathf.Sign(touchPositionWorldPoint.y - startPos.y);
			Vector2 swipeVector = Camera.main.ScreenToWorldPoint(touchPositionWorldPoint) - Camera.main.ScreenToWorldPoint(startPos);
			activePlayer.chargeJump(false);
			// Based on swipe direction, jump
			if(touchPositionWorldPoint.x >= activePlayer.transform.position.x && swipeDirection == -1 && couldBeSwipe && chargingJump){
				jumpRight(swipeVector);
				chargingJump = false;
			}else if(touchPositionWorldPoint.x < activePlayer.transform.position.x && swipeDirection == -1 && couldBeSwipe && chargingJump){
				jumpLeft(swipeVector);
				chargingJump = false;
			}
			// Cast a ray to check if the input is over an item or the player
			Ray _ray = Camera.main.ScreenPointToRay(realPosition);
			RaycastHit2D _hit = Physics2D.GetRayIntersection(_ray);
			// If the ray hit exists
			if(_hit){
				// Get the hit's collider
				Collider2D hitItemCol = _hit.collider;
				// Get the hit's gameobject
				GameObject tmpGo = hitItemCol.gameObject;
				// Get the collider's item component
				Item hitItem = hitItemCol.GetComponent<Item>();
				
				// If the item component exists, check proximity to item
				if(hitItem != null){
					Debug.Log ("ITEM EAT");
					// Distance check
					if ((activePlayer_go.transform.position.x + XEatTol) > hitItemCol.transform.position.x
					    && (activePlayer_go.transform.position.x - XEatTol) < hitItemCol.transform.position.x
					    && (activePlayer_go.transform.position.y + YEatTol) > hitItemCol.transform.position.y
					    && (activePlayer_go.transform.position.y - YEatTol) < hitItemCol.transform.position.y) {
						playerEat(hitItem);
					}
				}
				
				// Check if it's the player, if so throw up an item
				if(tmpGo != null){
					if(tmpGo.layer == LayerMask.NameToLayer("JellySprites")){
						Debug.Log("UPCHUCK!");
						// TODO: Uncomment throw up call here
						//playerUpChuck();
					}
				}
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
			active_PAnimator = jelly_Red_animator;
		}
		if (color.Equals ("blue")) {
			active_PAnimator = jelly_Blue_animator;
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
	void jumpLeft(Vector2 swipeVector){
		activePlayer.triggerJump("left", swipeVector);
		masterAnimationDel ("jumpLeft");
	}

	//jump right
	void jumpRight(Vector2 swipeVector){
		activePlayer.triggerJump("right", swipeVector);
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
			setActivePAni("blue");
			cameraPan.TriggerPan(Blue_GO, Red_GO);
			setInventory("blue");
		}
		else if (activePlayer == Blue_Player) {
			setActivePlayer("red");
			setActivePlayerGO("red");
			setActivePAni("red");
			cameraPan.TriggerPan(Red_GO, Blue_GO);
			setInventory("red");
		}
	}

	//function to delegate animations to animators in specific objects
	void masterAnimationDel (string aniAction){
		if (aniAction.Equals("jumpRight")){
			Debug.Log ("JUMP ANIM");
			active_PAnimator.SetInteger("Movement", 2);
			active_PAnimator.SetBool("isSick", false);
			active_PAnimator.SetBool("isVomiting", false);
		}
		if (aniAction.Equals("jumpLeft")){

			Debug.Log ("JUMP ANIM");
			active_PAnimator.SetInteger("Movement", 2);
			active_PAnimator.SetBool("isSick", false);
			active_PAnimator.SetBool("isVomiting", false);
		}

		// TODO: hook up death animation
		if (aniAction.Equals("death")){
			//active_PAnimator.Set_animation_state(???);
		}
		if (aniAction.Equals("walkLeft")){
			Debug.Log ("WALK ANIM");
			active_PAnimator.SetInteger("Movement", 1);
			//TODO faceing
		}
		if (aniAction.Equals("walkRight")){
			Debug.Log ("WALK ANIM");
			active_PAnimator.SetInteger("Movement", 1);
		}
		if (aniAction.Equals ("eat")) {
			Debug.Log ("EAT ANIM");
			active_PAnimator.SetBool("isEating", true);
		}
		if (aniAction.Equals ("upchuck")) {
			Debug.Log ("VOMIT ANIM");
			active_PAnimator.SetBool("isVomiting", true);
		}

		if(aniAction.Equals("idle")){
			active_PAnimator.SetInteger("Movement", 0);
			active_PAnimator.SetBool("isEating", false);
			active_PAnimator.SetBool("isVomiting", false);
		}
	}

}
