using UnityEngine;
using System.Collections;

public class Inventory_Blue : Inventory {

	// Use this for initialization
	void Start () {
		this.inventorySize = 100.0f;
		this.currentWeight = 0.0f;

		master = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterCtrl>();
		combiner = GameObject.FindGameObjectWithTag("Combiner").GetComponent<Item_Combiner>();
	}
	
	// Update is called once per frame
	void Update () {
		ItemDigestion();
	}
}
