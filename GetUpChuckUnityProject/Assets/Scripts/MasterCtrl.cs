using UnityEngine;
using System.Collections;

public class MasterCtrl : MonoBehaviour {

	//tracks color of active chuck
	//"red" or "blue"
	public string activeColor;

	//stored reference to red chuck
	GameObject RedCK;
	Player RedCKPlayer;

	//stored reference to blue chuck
	GameObject BlueCK;
	Player BlueCKPlayer;

	// Use this for initialization
	void Start () {
		BlueCK = GameObject.FindGameObjectWithTag("blueCK");
		RedCK = GameObject.FindGameObjectWithTag("redCK");
		RedCKPlayer = RedCK.GetComponent<Player> ();
		BlueCKPlayer = BlueCK.GetComponent<Player> ();
	
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
	
	}

	//getter for color
	public string getActiveColor(){
		return this.activeColor;
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
		if (activeColor.Equals ("red")) {
			RedCKPlayer.moveLeft();
				}
		if (activeColor.Equals ("blue")) {
			BlueCKPlayer.moveLeft();
		}
	}

	//walk right
	void walkRight(){
		if (activeColor.Equals ("red")) {
			RedCKPlayer.moveRight();
		}
		if (activeColor.Equals ("blue")) {
			BlueCKPlayer.moveRight();
		}
	}

	//jump left
	void jumpLeft(){
		if (activeColor.Equals ("red")) {
			RedCKPlayer.jump("left");
		}
		if (activeColor.Equals ("blue")) {
			BlueCKPlayer.jump("left");
		}
	}

	//jump right
	void jumpRight(){
		if (activeColor.Equals ("red")) {
			RedCKPlayer.jump("right");
		}
		if (activeColor.Equals ("blue")) {
			BlueCKPlayer.jump("right");
		}
	}

	//called when player dies
	void playerDeath (){
		if (activeColor.Equals ("red")) {
			RedCKPlayer.playerRe();
		}
		if (activeColor.Equals ("blue")) {
			BlueCKPlayer.playerRe();
		}
	}

	//called to switch player from red to blue or blue to red
	void swapPlayer(){
		if (activeColor.Equals ("red")) {
			activeColor = "blue";
		}
		else if (activeColor.Equals ("blue")) {
			activeColor = "red";
		}
	}


}
