using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//boolean to handle color true=red false=blue
	public bool colorRed;

	//intance of player inventory used for this player
	Inventory playerInventory;

	//max inventory size stored for model calculations
	float maxInvSize;

	//force of jumps
	public int jumpForce;
	public float jumpAngle;

	//respawn x and y stored when player reaches a checkpoint
	public float reX;
	public float reY;

	//last y position, used to prevent double jump
	private float lastY;
	private float lastYTwo;

	//movespeed of the character
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		if (this.colorRed == true) {
			playerInventory = this.GetComponent<Inventory_Red> ();
		}
		if (this.colorRed == false) {
			playerInventory = this.GetComponent<Inventory_Blue> ();
		}
		maxInvSize = playerInventory.inventorySize;
		lastY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		lastYTwo = lastY;
		lastY = this.transform.position.y;
	}

	//chages size of character if weight reaches a threshhold
	//TODO call on eat or throwup
	void changeSize (float sizeIn){
		float tmpMax = (float)maxInvSize;

		float scaleFactor = sizeIn / tmpMax;

		this.transform.localScale += new Vector3 (scaleFactor, scaleFactor, 0.0f);
		
	}
	
	//moves the player right
	//TODO flip sprite to face correct direction
	public void moveRight(){
		Debug.Log("MOVE RIGHT");
		Vector3 dir = Quaternion.AngleAxis(15, Vector3.forward) * Vector3.right;
		this.GetComponent<JellySprite>().AddForce(dir*moveSpeed);
	}

	//moves the player left
	public void moveLeft(){
		Debug.Log("MOVE LEFT");
		Vector3 dir = Quaternion.AngleAxis(15, Vector3.forward) * Vector3.left;
		this.GetComponent<JellySprite>().AddForce(dir*moveSpeed);
	}
	
	//called to make the player jump
	//takes direction as a string
	public void jump(string Direc){
		//make sure the player is not already jumping or falling
		if (lastY == lastYTwo) {
			if (Direc.Equals ("right")) {
				Vector3 dir = Quaternion.AngleAxis(jumpAngle, Vector3.forward) * Vector3.right;
				this.GetComponent<JellySprite>().AddForce(dir*jumpForce);
				//this.rigidbody2D.AddForce (dir*jumpForce);
			}

			if (Direc.Equals ("left")) {
				Vector3 dir = Quaternion.AngleAxis(jumpAngle, Vector3.back) * Vector3.left;
				this.GetComponent<JellySprite>().AddForce(dir*jumpForce);
				//this.rigidbody2D.AddForce (dir*jumpForce);
			}
		}
	}

	//called by a checkpoint to set the respawn point
	public void setCkPt(float tmpX, float tmpY){
		this.reX = tmpX;
		this.reY = tmpY;
		}

	//called when a player is forced to respawn (like on death)
	public void playerRe(){
		Debug.Log("RESPAWN");
		this.transform.position = new Vector3 (reX, reY, this.transform.position.z);
	}


	//geter and seter for colorRed
	public bool getColorRed(){
		return this.colorRed;
	}

	public void setColorRed(string tmpColor){
		if (tmpColor.Equals ("red")) {
			this.colorRed = true;
		}
		if (tmpColor.Equals ("blue")) {
			this.colorRed = false;
		}
	}

}
