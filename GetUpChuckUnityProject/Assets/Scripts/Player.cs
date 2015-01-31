using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//boolean to handle color true=red false=blue
	public bool colorRed;

	//intance of player inventory used for this player
	Inventory playerInventory;

	//max inventory size stored for model calculations
	int maxInvSize;

	//force of jumps
	public int jumpForce;

	//respawn x and y stored when player reaches a checkpoint
	public float reX;
	public float reY;

	//==================================================================

	// Use this for initialization
	void Start () {
		playerInventory = this.GetComponent<Inventory> ();
		maxInvSize = playerInventory.inventorySize;
	}
	
	// Update is called once per frame
	void Update () {
	
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
		this.transform.Translate (Vector3.right * Time.deltaTime);
		}

	//moves the player left
	public void moveLeft(){
		this.transform.Translate (Vector3.left * Time.deltaTime);
		}
	
	//called to make the player jump
	//takes direction as a string
	public void jump(string Direc){
		//TODO make sure the player is not already jumping or falling

		if (Direc.Equals ("right")) {
			this.rigidbody.AddForce(new Vector3(jumpForce, jumpForce,0));
				}

		if (Direc.Equals ("left")) {
			this.rigidbody.AddForce(new Vector3(-jumpForce, jumpForce,0));
			}
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
