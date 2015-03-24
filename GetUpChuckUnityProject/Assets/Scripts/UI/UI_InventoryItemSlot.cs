using UnityEngine;
using System.Collections;

public class UI_InventoryItemSlot : MonoBehaviour {

	public bool ItemSelected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool getItemSelection(){
		return ItemSelected;
	}

	public void setItemSelection(bool b){
		ItemSelected = b;
	}

	public void ItemClicked(){
		ItemSelected = !ItemSelected;
	}
}
