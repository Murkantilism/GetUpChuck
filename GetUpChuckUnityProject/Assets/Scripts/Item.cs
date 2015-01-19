using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

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
	public string[] potentialCombinations;

	// A timer to keep track of when a second of game time has occurred
	float oneSecTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// If the item can be digested 
		if(digestionTimer != 1337){
			// Increment timer
		    oneSecTimer += Time.deltaTime;
			// Once a second of game time is reached, call DigestionTick()
			if(oneSecTimer > 1){
				// TODO: Get a reference to the Inventory classes for Red & Blue
				// then call the DigestionTick() function, passing ourself as the arg
			}
			oneSecTimer = 0; // Reset timer
		}
	}

	//geter and seter for weight
	public float getWeight (){
		return this.weight;
	}
	public void setWeight(float inWeight){
		this.weight = inWeight;
	}

	//geter and seter for hue
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

	//geter and seter for name
	public string getName(){
		return this.item_name;
	}
	public void setName(string tmpName){
		this.item_name = tmpName;
	}

	// Geter and seter for digestion timer
	public float getDigestionTimer(){
		return this.digestionTimer;
	}
	public void setDigestionTimer(float tmpTimer){
		this.digestionTimer = tmpTimer;
	}

	// Geter and seter for list of combinations
	public string[] getCombinations(){
		return this.potentialCombinations;
	}
	public void setCombinations(string[] tmpCombos){
		this.potentialCombinations = tmpCombos;
	}

	// Add this item to the inventory system
	void AddToInventory(){
		// TODO: Call the AddItem() function of the target inventory script
		// TODO: Call the HonorableSeppuku() function once item is added
	}

	// Item deletes iteself from world space after it has been added to inventory 
	void HonorableSeppuku(){
		Destroy(gameObject);
		Destroy(this);
	}

}
