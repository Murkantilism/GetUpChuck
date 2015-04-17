using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//boolean to handle color true=red false=blue
	public bool colorRed;
	
	//intance of player inventory used for this player
	public Inventory playerInventory;
	
	//max inventory size stored for model calculations
	public float maxInvSize;
	
	//force of jumps
	public int jumpForce;
	public float jumpAngle;
	public int jumpSquash;
	
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
		maxInvSize = 100;
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
		//Debug.Log ("Move right triggered.");
		Vector3 dir = Quaternion.AngleAxis(15, Vector3.forward) * Vector3.right;
		this.GetComponent<JellySprite>().AddForce(dir*moveSpeed);
	}
	
	//moves the player left
	public void moveLeft(){
		//Debug.Log ("Move left triggered.");
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
	public void chargeJump(bool charging){
		//Debug.Log ("Charging jump.");
		if(charging){
			this.GetComponent<JellySprite>().AddForce(-Vector2.up*jumpSquash);
		}
	}

	// Once the charged jump is released trigger it, apply force with
	// the inverse of the swipe vector.
	public void triggerJump(string Direc, Vector2 swipeVector){
		if (Direc.Equals("right")){
			this.GetComponent<JellySprite>().AddForce(-swipeVector*jumpForce);
		}else{
			this.GetComponent<JellySprite>().AddForce(-swipeVector*jumpForce);
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
		MoveJellySpriteToPosition(this.GetComponent<JellySprite>(), new Vector3 (reX, reY, this.transform.position.z), true);
	}

	// A special function to move a Jelly Sprite to the given position.
	void MoveJellySpriteToPosition(JellySprite jellySprite, Vector3 position, bool resetVelocity)
	{
		Vector3 offset = position - jellySprite.CentralPoint.GameObject.transform.position;
		
		foreach(JellySprite.ReferencePoint referencePoint in jellySprite.ReferencePoints)
		{
			if(!referencePoint.IsDummy)
			{
				referencePoint.GameObject.transform.position = referencePoint.GameObject.transform.position + offset;
				
				if(resetVelocity)
				{
					if(referencePoint.Body2D)
					{
						referencePoint.Body2D.angularVelocity = 0.0f;
						referencePoint.Body2D.velocity = Vector2.zero;
					}
					else if(referencePoint.Body3D)
					{
						referencePoint.Body3D.angularVelocity = Vector3.zero;
						referencePoint.Body3D.velocity = Vector3.zero;
					}
				}
			}
		}
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
