using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {
	//stored reference to red
	GameObject Red_GO;
	Player Red_Player;
	Inventory RedInv;
	Player_Animator Red_animator;

	//stored reference to blue
	GameObject Blue_GO;
	Player Blue_Player;
	Inventory BlueInv;
	Player_Animator Blue_animator;

	// Tracks which player is currently active
	public GameObject activePlayer_go;
	public Player activePlayer;
	Inventory activeInventory;
	Player_Animator active_PAnimator;


	//camera info
	Camera mainCam;
	public CameraPan cameraPan;
	CameraZoom cameraZoom;
	CameraZoomOut cameraZoomOut;

	//tracks position for mouse drags
	Vector2 mouseDragStart;
	Vector2 defMDS = new Vector2 (-1.0f, -1.0f);

	//item pickup ranges
	public float XEatTol;
	public float YEatTol;

	//variables for hold for opening inventory in touch
	bool isIncrementing;
	int incCount;
	public int holdTimeForInv = 200;

	//keeps track if the inv is open or not
	public bool isInvOpen; //def: false
	//keeps track of what action is being held
	string holdAction;  //def: "none"
	//"OpenInv" - open inventory

	//tracks the direction the player is facing
	public bool faceingRight = true;

	// Use this for initialization
	void Start () {

		Blue_GO = GameObject.Find("bluePlayer");
		Blue_Player = Blue_GO.GetComponent<Player> ();
		Red_GO = GameObject.Find("redPlayer");
		Red_Player = Red_GO.GetComponent<Player> ();

		RedInv = Red_GO.GetComponent<Inventory> ();
		BlueInv = Blue_GO.GetComponent<Inventory> ();

		Red_animator = Red_GO.GetComponent<Player_Animator> ();
		Blue_animator = Blue_GO.GetComponent<Player_Animator> ();

		// Set red to active player first
		setActivePlayer("red");
		setActivePlayerGO("red");
		setInventory ("red");
		setActivePAni ("red");

		mouseDragStart = defMDS;
		isIncrementing = false;
		incCount = 0;

		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		cameraPan = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPan>();
		cameraZoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoom>();
		cameraZoomOut = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoomOut>();

		isInvOpen = false;
		holdAction = "none";
	}
	
	// Update is called once per frame
	void Update () {

		if (!isInvOpen) {
			if (Input.GetButtonDown ("playerSwaps")) {
				swapPlayer ();
			}

#if UNITY_EDITOR || UNITY_STANDALONE

		//walk controls (mouse)
		if (Input.GetMouseButton (0)) {
			if (mouseDragStart == defMDS) {
				if (0 < Input.mousePosition.x && Input.mousePosition.x < (mainCam.pixelWidth / 3)) {
					walkLeft ();
				}
				if ((2 * mainCam.pixelWidth / 3) < Input.mousePosition.x && Input.mousePosition.x < mainCam.pixelWidth) {
					walkRight ();
				}
			}
		}

		//click to add item to inventory (mouse) / inventory open also started
		if (Input.GetMouseButtonDown (0)) {
			if ((mainCam.pixelWidth / 3) < Input.mousePosition.x && Input.mousePosition.x < (2 * mainCam.pixelWidth / 3)) {
				mouseDragStart = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.GetRayIntersection (ray);
				if (hit) {
					//set hitItem to item hit with touch
					Collider2D hitItemCol = hit.collider;
					Item hitItem = hitItemCol.GetComponent<Item> ();
					GameObject tmp = hitItemCol.gameObject;
					//if hitItem is not null (item is hit) add it
					if (hitItem != null) {
						//distance check
						if ((activePlayer_go.transform.position.x + XEatTol) > hitItemCol.transform.position.x
							&& (activePlayer_go.transform.position.x - XEatTol) < hitItemCol.transform.position.x
							&& (activePlayer_go.transform.position.y + YEatTol) > hitItemCol.transform.position.y
							&& (activePlayer_go.transform.position.y - YEatTol) < hitItemCol.transform.position.y) {
								playerEat(hitItem);
						}
					}
					//checks if active player is being selected (for inventory open)
					if (tmp != null) {
						if (tmp == activePlayer_go) {
							isIncrementing = true;
							holdAction = "OpenInv";
						}
					}
				}
			}
		}

		//jump controls (mouse) / also ends inventory open early
		if (Input.GetMouseButtonUp (0)) {
			if (mouseDragStart != defMDS) {
				if (0 < Input.mousePosition.x && Input.mousePosition.x < (mainCam.pixelWidth / 3)) {
					jumpLeft ();
				}
				if ((2 * mainCam.pixelWidth / 3) < Input.mousePosition.x && Input.mousePosition.x < mainCam.pixelWidth) {
					jumpRight ();
				}
			}
			mouseDragStart = defMDS;
			//if not holding for long enough for click and 
			isIncrementing = false;
			incCount = 0;
			holdAction = "none";
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
									playerEat(hitItem);
							}
						}
						//checks if active player is being selected (for inventory open)
						if (tmp != null){
							if (tmp == activePlayer_go){
								isIncrementing = true;
								holdAction = "OpenInv";
							}
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
					holdAction = "none";
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
				if (holdAction.Equals ("OpenInv")) {
					openInventory();
					isInvOpen = true;
					//TODO set isInvOpen to false when closing inventory
				}
				holdAction = "none";
			}


		} //Close !isInvOpen


		if (Input.GetKeyDown(KeyCode.Z)){
			openInventory();
		}
		if (Input.GetKeyDown(KeyCode.X)){
			closeInventory();
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
		if (aniAction.Equals("jumpRight")){
			active_PAnimator.Set_animation_state(2);
		}
		if (aniAction.Equals("jumpLeft")){
			active_PAnimator.Set_animation_state(2);
		}
		if (aniAction.Equals("death")){
			//active_PAnimator.Set_animation_state(???);
		}
		if (aniAction.Equals("walkLeft")){
			active_PAnimator.Set_animation_state(1);
			if(faceingRight == true){
				activePlayer_go.transform.localScale += new Vector3( -(2 * activePlayer_go.transform.localScale.x),0,0);
			}
			faceingRight = false;
		}
		if (aniAction.Equals("walkRight")){
			active_PAnimator.Set_animation_state(1);
			if(faceingRight == false){
				activePlayer_go.transform.localScale += new Vector3( -(2 * activePlayer_go.transform.localScale.x),0,0);
			}
			faceingRight = true;
		}
		if (aniAction.Equals ("eat")) {
			active_PAnimator.Set_animation_state(3);
		}
		if (aniAction.Equals ("upchuck")) {
			active_PAnimator.Set_animation_state(4);
		}

	}

}
