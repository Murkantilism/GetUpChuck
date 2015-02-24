﻿using UnityEngine;
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

	//item pickup ranges
	public float XEatTol;
	public float YEatTol;

	//variables for hold for opening inventory in touch
	bool isIncrementing;
	int incCount;
	public int holdTimeForInv;

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
		isIncrementing = false;
		incCount = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("playerSwaps")){
			swapPlayer();
		}

#if UNITY_EDITOR || UNITY_STANDALONE

		//walk controls (mouse)
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

		//click to add item to inventory (mouse) / inventory open also started
		if (Input.GetMouseButtonDown (0)) {
			if ((mainCam.pixelWidth/3) < Input.mousePosition.x && Input.mousePosition.x < (2*mainCam.pixelWidth/3)){
				mouseDragStart = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
				if (hit) {
					//set hitItem to item hit with touch
					Collider2D hitItemCol = hit.collider;
					Item hitItem = hitItemCol.GetComponent<Item>();
					GameObject tmp = hitItemCol.gameObject;
					//if hitItem is not null (item is hit) add it
					if (hitItem != null){
						//distance check
						if((activePlayer_go.transform.position.x + XEatTol) > hitItemCol.transform.position.x
						   && (activePlayer_go.transform.position.x - XEatTol) < hitItemCol.transform.position.x
						   && (activePlayer_go.transform.position.y + YEatTol) > hitItemCol.transform.position.y
						   && (activePlayer_go.transform.position.y - YEatTol) < hitItemCol.transform.position.y){
							activeInventory.AddItem(hitItem);
						}
					}
					//checks if active player is being selected (for inventory open)
					if (tmp == activePlayer_go){
						isIncrementing = true;
					}
				}
			}
		}

		//jump controls (mouse) / also ends inventory open early
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
			//if not holding for long enough for inventory open
			isIncrementing = false;
			incCount = 0;
		}

#endif

#if UNITY_IPHONE || UNITY_ANDROID

		//touch controls
		if(Input.touchCount > 0) {
			if ((Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetTouch(0).phase == TouchPhase.Stationary)) {
				//walking left
				if (0 < Input.GetTouch(0).position.x && Input.GetTouch(0).position.x < (mainCam.pixelWidth/3)){
					walkLeft();
				}
				//walking right
				if ((2*mainCam.pixelWidth/3) < Input.GetTouch(0).position.x && Input.GetTouch(0).position.x < mainCam.pixelWidth){
					walkRight();
				}
			}
			if (Input.GetTouch(0).phase == TouchPhase.Began){
				//eating (and inventory open)
				if ((mainCam.pixelWidth/3) < Input.GetTouch(0).position.x && Input.GetTouch(0).position.x < (2*mainCam.pixelWidth/3)){
					mouseDragStart = Input.GetTouch(0).position;
					Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
					RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
					if (hit) {
						Collider2D hitItemCol = hit.collider;
						//set hitItem to item hit with touch
						Item hitItem = hitItemCol.GetComponent<Item>();
						GameObject tmp = hitItemCol.gameObject;
						//if hitItem is not null (item is hit) add it
						if (hitItem != null){
							//distance check
							if((activePlayer_go.transform.position.x + XEatTol) > hitItemCol.transform.position.x
							   && (activePlayer_go.transform.position.x - XEatTol) < hitItemCol.transform.position.x
							   && (activePlayer_go.transform.position.y + YEatTol) > hitItemCol.transform.position.y
							   && (activePlayer_go.transform.position.y - YEatTol) < hitItemCol.transform.position.y){
								activeInventory.AddItem(hitItem);
							}
						}
						//checks if active player is being selected (for inventory open)
						if (tmp == activePlayer_go){
							isIncrementing = true;
						}
					}
				}
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended){
				if (mouseDragStart != defMDS){
					//jump left
					if (0 < Input.GetTouch(0).position.x && Input.GetTouch(0).position.x < (mainCam.pixelWidth/3)){
						jumpLeft();
					}
					//jump right
					if ((2*mainCam.pixelWidth/3) < Input.GetTouch(0).position.x && Input.GetTouch(0).position.x < mainCam.pixelWidth){
						jumpRight();
					}
				}
				mouseDragStart = defMDS;
			}
			if (Input.GetTouch(0).phase != TouchPhase.Stationary){
				//if not holding and was holding for inv open, stop and reset
				if (incCount > 0){
					isIncrementing = false;
					incCount = 0;
				}
			}
		}

#endif

		//increments incCount for inventory open
		if (isIncrementing == true) {
			incCount = incCount + 1;
		}

		//if have been holding down on chuck long enough, open inventory
		//TODO hook
		if (incCount > holdTimeForInv) {
			isIncrementing = false;
			incCount = 0;
			//TODO OPEN INVENTORY HOOK HERE
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
