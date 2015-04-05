using UnityEngine;
using System.Collections;

public class Inventory_New : MonoBehaviour {

	//stores current inventory weight
	int currentWeight;

	//game object prefabs
	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;

	GameObject[] prefabsAR;

	//stores the stock item prefab
	//http://answers.unity3d.com/questions/48941/randomly-pick-then-create-prefab.html
	//http://docs.unity3d.com/Manual/InstantiatingPrefabs.html

	// Use this for initialization
	void Start () {
		currentWeight = 0;

		prefabsAR [0] = prefab1;
		prefabsAR [1] = prefab2;
		prefabsAR [2] = prefab3;
		
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
			GameObject vomitI = prefabsAR[Random.Range(0, prefabsAR.Length)];
			GameObject VItem = (GameObject) Instantiate(vomitI, transform.position, transform.rotation);
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
