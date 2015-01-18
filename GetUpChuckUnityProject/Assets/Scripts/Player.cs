using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//boolean to handle color true=red false=blue
	public bool colorRed;

	//set the current size of the player
	//1 is the starting size with an empty inventory
	int size;

	// threshholds for going up to the next size level
	/*
	public int sizeThresh1;
	public int sizeThresh2;
	public int sizeThresh3;
	*/

	//intance of player inventory used for this player
	Inventory playerInventory;

	// Use this for initialization
	void Start () {
		playerInventory = this.GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//chages size of character if weight reaches a threshhold
	//TODO call on eat or throwup
	void changeSize (){
		int tmpCurrInvSize = playerInventory.getCurrentWeight ();
		int tmpMaxInvSize = playerInventory.inventorySize;
		float tmpCurrF = (float) tmpCurrInvSize;
		float tmpMax = (float)tmpMaxInvSize;

		float scaleFactor = tmpCurrF / tmpMax;

		this.transform.localScale += new Vector3 (scaleFactor, scaleFactor, 0.0f);

		/*if (tmpInvSize > this.sizeThresh1) {
			if (tmpInvSize > this.sizeThresh2) {
				if (tmpInvSize > this.sizeThresh3) {
					this.size = 4;
				}
				else {
					this.size = 3;
				}
			}
			else {
				this.size = 2;
			}
		}*/
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

	//TODO apply force
	//called to make the player jump
	public void jump(){
		//make sure the player is not already jumping or falling

		//rigidbody.addforce
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

	//geter and seter for size
	public int getSize(){
		return this.size;
	}

	public void setSize(int tmpSize){
		this.size = tmpSize;
	}

}
