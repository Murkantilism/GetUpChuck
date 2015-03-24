using UnityEngine;
using System.Collections;

public class UI_InventoryUpchuck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenInventory(){
		this.gameObject.SetActive(true);
	}
	
	public void CloseInventory(){
		this.gameObject.SetActive(false);
	}

	// Setup the initial inventory
	public void CreateInventory(){
		// Get reference to inventory transform
		RectTransform inventoryRect = GetComponent<RectTransform>();
		// Set size of inventory
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200); // FIXME change from hardcoded values
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 500);
	}
}
