﻿using UnityEngine;
using System.Collections;

public abstract class Inventory : MonoBehaviour {

	//inventory size (max weight)
	public float inventorySize;

	//keep traks of how much weight the player is currently carrying
	public float currentWeight;

	// Declare ArrayList to store list of items
	public ArrayList loItems = new ArrayList();

	GameObject inst_item;

	// A timer to keep track of when a second of game time has occurred
	float oneSecTimer;

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
		if(currentWeight > item.getWeight()){ // Make sure we don't go to negative weight
		    currentWeight -= item.getWeight();
		}
		// Instantiate item in world space
		InstantiateItem(item);
	}

	// Instantiate the item in world space directly in front of player character
	void InstantiateItem(Item item){
		// Set a temp gameObject from resources folder
		inst_item = Resources.Load<GameObject>(item.getName());
		// TODO: Once player class has been implemeneted, set a reference to player class
		// and instantiate in front of player character instead of 0, 0
		Instantiate(inst_item, new Vector2(0, 0), Quaternion.identity);
	}

	// Delete the item from inventory, DO NOT drop it back into world space
	public void DeleteItem(Item item){
		loItems.Remove(item);
		if(currentWeight > item.getWeight()){ // Make sure we don't go to negative weight
		    currentWeight -= item.getWeight();
		}
	}

	// For every item currently in the inventory that can be digested,
	// call the DigestionTick() function on it.
	public void ItemDigestion(){
		foreach(Item item in loItems){
			// If the item can be digested
			if(item.digestionTimer != 1337.0f){
				// Increment timer
				oneSecTimer += Time.deltaTime;
				// Once a sec of game time is reached, call DigestionTick()
				if(oneSecTimer > 1){
					DigestionTick(item);
					oneSecTimer = 0; // Reset timer
				}
			}
		}
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

	// Given an Array of 2 items check if item 2 is a member of item 1's list of potential combos
	public void CheckItemCombinations(ArrayList items){

		Item item0 = (Item) items[0];
		Item item1 = (Item) items[1];

		// For every element of the potential combination list, get the first item in
		// the tuple and compare it against the item we are checking against (item1).
		for (int i = 0; i < item0.potentialCombinations.Count; i++){
			System.Tuple<string, string> tmpTuple = (System.Tuple<string, string>) item0.getCombinations()[i];

			if(tmpTuple.Item1.Contains(item1.getName())){
				Debug.Log("Successful combo: " + item0.getName() + " + " + item1.getName());
				CombineItems(items);
			}
		}
	}

	public void CombineItems(ArrayList items){

	}
}
