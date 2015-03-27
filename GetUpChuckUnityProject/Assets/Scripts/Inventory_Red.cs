using UnityEngine;
using System.Collections;

public class Inventory_Red : Inventory {

	// Use this for initialization
	void Start () {
		this.inventorySize = 100.0f;
		this.currentWeight = 0.0f;

		master = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
		ItemDigestion();

		foreach (Item item in loItems){
			Debug.Log(item.getName());
		}
	}
}
