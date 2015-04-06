using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	//stores current inventory weight
	public int currentWeight;

	//game object prefabs
	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;

	public GameObject[] prefabsAR;

	//stores the stock item prefab
	//http://answers.unity3d.com/questions/48941/randomly-pick-then-create-prefab.html
	//http://docs.unity3d.com/Manual/InstantiatingPrefabs.html

	// Use this for initialization
	void Awake () {
		//this needs to be awake and not start because this is not attached to any game objects
		currentWeight = 0;

		prefabsAR = new GameObject[3];

		prefabsAR[0] = prefab1;
		prefabsAR[1] = prefab2;
		prefabsAR[2] = prefab3;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (prefabsAR.Length);
	}

	//call when chuck eats a non key item
	public void invEat(){
		currentWeight = currentWeight + 1;
	}

	public void vomit(){
		if (currentWeight > 0) {
			currentWeight = currentWeight - 1;
			Debug.Log (prefabsAR.Length);
			GameObject vomitI = prefabsAR[Random.Range(0, prefabsAR.Length)];
			GameObject VItem = (GameObject) Instantiate(vomitI, transform.position, transform.rotation);
		}
	}

	//getters and setters

	public int getCurrentWeight(){
		return currentWeight;
	}

	public void setWeight(int tmpW){
		currentWeight = tmpW;
	}
}
