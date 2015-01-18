using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	//inventory size (max weight)
	public int inventorySize;

	//keep traks of how much weight the player is currently carrying
	public int currentWeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//TODO structure to store items in

	//TODO add items to structure, increment weight

	//TODO remove items from structure, decrease weight

	//geter ans seterfor currentWeight
	public int getCurrentWeight(){
		return this.currentWeight;
	}

	public void setCurrentWeight(int tmpWeight){
		this.currentWeight = tmpWeight;
		}

}
