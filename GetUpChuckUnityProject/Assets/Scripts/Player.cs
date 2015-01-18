using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//boolean to handle color true=red false=blue
	public bool colorRed;

	//set the current size of the player
	//1 is the starting size with an empty inventory
	int size;

	//threshholds for going up to the next size level
	public int sizeThresh1;
	public int sizeThresh2;
	public int sizeThresh3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//chages size of character if weight reaches a threshhold
	//TODO call on eat or throwup
	void changeSize (){
			/*if (INV > this.sizeThresh1) {
						if (INV > this.sizeThresh2) {
								if (INV > this.sizeThresh3) {
									
								}
						}
				}*/
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
