using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	//stores weight of item
	public float weight;

	//stores hue
	//red = 0
	//blue = 1
	public int hue;

	//name of object
	public string item_name;

	// How long it takes for the item to degrade (if set to 1337 item cannot be digested)
	public float digestionTimer;

	// An array of potential item combinations allowed with this item
	public ArrayList potentialCombinations;

	public Sprite sprite;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	/**********************
	 * Geters and Seters *
	**********************/
	public float getWeight (){
		return this.weight;
	}
	public void setWeight(float inWeight){
		this.weight = inWeight;
	}

	public int getHue(){
		return this.hue;
	}
	public void setHue (string tmpHue){
		if (tmpHue.Equals ("red")) {
			this.hue = 0;
		}
		if (tmpHue.Equals ("blue")) {
			this.hue = 1;
		}
	}

	public string getName(){
		return this.item_name;
	}
	public void setName(string tmpName){
		this.item_name = tmpName;
	}

	public float getDigestionTimer(){
		return this.digestionTimer;
	}
	public void setDigestionTimer(float tmpTimer){
		this.digestionTimer = tmpTimer;
	}

	public ArrayList getCombinations(){
		return this.potentialCombinations;
	}
	public void setCombinations(ArrayList tmpCombos){
		this.potentialCombinations = tmpCombos;
	}

	// Given which character is active, add this item to their inventory system
	// and disable the renderer to fake deletion of item.
	public void AddToInventory(Inventory playerInventory){
		playerInventory.AddItem(this);
		this.renderer.enabled = false;
	}
}
