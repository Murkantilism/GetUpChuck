using UnityEngine;
using System.Collections;

public class Inventory_New : MonoBehaviour {

	//stores current inventory weight
	int currentWeight;

	//stores the stock item prefab
	//http://answers.unity3d.com/questions/48941/randomly-pick-then-create-prefab.html
	//http://docs.unity3d.com/Manual/InstantiatingPrefabs.html

	// Use this for initialization
	void Start () {
		currentWeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//call when chuck eats a non key item
	public void invEat(){
		currentWeight = currentWeight + 1;
	}

	public void vomit(){
		if (currentWeight > 0) {
			currentWeight = currentWeight - 1;
			//TODO spawn stock item prefab
		}
	}

	//getters and setters

	public int getWeight(){
		return currentWeight;
	}

	public void setWeight(int tmpW){
		currentWeight = tmpW;
	}
}
