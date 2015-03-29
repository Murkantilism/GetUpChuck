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
	
	public bool canJump = true;
	float jumpDelay = 1.25f;
	
	//movespeed of the character
	public float moveSpeed;
	
	//==================================================================
	
	// Use this for initialization
	void Start () {
		if (this.colorRed == true) {
			playerInventory = this.GetComponent<Inventory_Red> ();
		}
		if (this.colorRed == false) {
			playerInventory = this.GetComponent<Inventory_Blue> ();
		}
		maxInvSize = playerInventory.inventorySize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//chages size of character with weight 
	//TODO call on eat or throwup
	public void changeSize (float sizeIn){
		float tmpMax = (float)maxInvSize;
		
		float scaleFactor = sizeIn / tmpMax;
		
		this.transform.localScale += new Vector3 (scaleFactor, scaleFactor, 0.0f);
	}
	
	//moves the player right
	//TODO flip sprite to face correct direction
	public void moveRight(){
		Debug.Log ("Move right triggered.");
		Vector3 dir = Quaternion.AngleAxis(15, Vector3.forward) * Vector3.right;
		this.GetComponent<JellySprite>().AddForce(dir*moveSpeed);
	}
	
	//moves the player left
	public void moveLeft(){
		Debug.Log ("Move left triggered.");
		Vector3 dir = Quaternion.AngleAxis(15, Vector3.forward) * Vector3.left;
		this.GetComponent<JellySprite>().AddForce(dir*moveSpeed);
	}
	
	//called to make the player jump
	//takes direction as a string
	public void jump(string Direc){
		if (Direc.Equals ("right") && canJump == true) {
			Vector3 dir = Quaternion.AngleAxis(jumpAngle, Vector3.forward) * Vector3.right;
			this.GetComponent<JellySprite>().AddForce(dir*jumpForce);
			StartCoroutine("DisableJumpTmp");
		}
		
		if (Direc.Equals ("left") && canJump == true) {
			Vector3 dir = Quaternion.AngleAxis(jumpAngle, Vector3.back) * Vector3.left;
			this.GetComponent<JellySprite>().AddForce(dir*jumpForce);
			StartCoroutine("DisableJumpTmp");
		}
	}

	// Charge the jump by applying down force
	public void chargeJump(){
		Debug.Log ("Charging jump.");
		this.GetComponent<JellySprite>().AddForce(-Vector2.up*jumpForce);
	}

	// Once the charged jump is released trigger it, apply force in left/right dir
	public void triggerJump(string Direc){
		if (Direc.Equals("right")){
			Debug.Log ("Right jump triggered.");
			this.GetComponent<JellySprite>().AddForce(Vector3.right*1.5f*jumpForce);
		}else{
			Debug.Log ("Left jump triggered.");
			this.GetComponent<JellySprite>().AddForce(Vector3.left*1.5f*jumpForce);
		}
	}
	
	private IEnumerator DisableJumpTmp(){
		canJump = false;
		yield return new WaitForSeconds(jumpDelay);
		canJump = true;
	}
	
	//called by a checkpoint to set the respawn point
	public void setCkPt(float tmpX, float tmpY){
		this.reX = tmpX;
		this.reY = tmpY;
	}
	
	//called when a player is forced to respawn (like on death)
	public void playerRe(){
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
