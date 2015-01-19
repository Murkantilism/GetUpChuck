using UnityEngine;
using System.Collections;

public abstract class Inventory : MonoBehaviour {

	//inventory size (max weight)
	public float inventorySize;

	//keep traks of how much weight the player is currently carrying
	public float currentWeight;

	// Declare ArrayList to store list of items
	ArrayList loItems = new ArrayList();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Geter and seter for currentWeight
	public float getCurrentWeight(){
		return this.currentWeight;
	}

	public void setCurrentWeight(float tmpWeight){
		this.currentWeight = tmpWeight;
	}

	// Add items to structure, increment weight
	public void AddItem(Item item){
		loItems.Add(item);
		currentWeight += item.getWeight();
	}
	
	//Remove item from structure, decrease weight
	public void DropItem(Item item){
		loItems.Remove(item);
		currentWeight -= item.getWeight();
		// TODO: Call a function to instantiate item in world space
	}

	// Delete the item from inventory, DO NOT drop it back into world space
	public void DeleteItem(Item item){
		loItems.Remove(item);
		currentWeight -= item.getWeight();
	}

	// Called by each individual item once per second of game time.
	public void DigestionTick(Item item){
		// If the timer hasn't expired, subtract 1 from the digst timer
		if(item.getDigestionTimer() > 0){
			item.setDigestionTimer(item.getDigestionTimer() - 1.0f);
		// If the timer has expired, delete the item fromt the inventory
		}else if(item.getDigestionTimer() <= 0){
			DeleteItem(item);
		}
	}

	// Given an Array of items check if any can be combined with each other.
	void CheckItemCombinations(Item[] items){

	}

}
