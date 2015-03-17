using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour {

	private RectTransform inventoryRect;
	private float inventoryW, inventoryH;

	public int slots, rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;

	public GameObject slotPrefab;

	private List<GameObject> inventorySlots;

	Inventory_Blue blue_inventory;
	Inventory_Red red_inventory;

	CameraZoom cameraZoom;
	CameraZoomOut cameraZoomOut;

	bool firstTime = true; // First time opening inventory?

	// Use this for initialization
	void Start () {
		blue_inventory = GameObject.FindGameObjectWithTag("blue").GetComponent<Inventory_Blue>();
		red_inventory = GameObject.FindGameObjectWithTag("red").GetComponent<Inventory_Red>();

		cameraZoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoom>();
		cameraZoomOut = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoomOut>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OpenInventory(Inventory activeInventory){
		// If we camera isn't already zooming out, open inventory UI
		if(cameraZoomOut.GetZoomState() == false){
			// If this is the first time we are opening inventory, create it
			if(firstTime){
				CreateInventory(activeInventory);
				firstTime = false;
			// Otherwise just re-enable it
			}else{
				// For every slot in the inventory, enable it
				for (int j = 0; j < inventorySlots.Count; j++){
					inventorySlots[j].gameObject.SetActive(true);
				}
				this.gameObject.SetActive(true);
			}
		}
	}

	public void CloseInventory(Inventory activeInventory){
		// If we camera isn't already zooming in, close inventory UI
		if(cameraZoom.GetZoomState() == false){
			// For every slot in the inventory, disable it
			for (int j = 0; j < inventorySlots.Count; j++){
				inventorySlots[j].gameObject.SetActive(false);
			}
			this.gameObject.SetActive(false);
		}
	}


	// Setup the initial inventory
	private void CreateInventory(Inventory activeInventory){
		inventorySlots = new List<GameObject>();

		// Calculate inventory width and height
		inventoryW = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryH = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

		// Get reference to inventory transform
		inventoryRect = GetComponent<RectTransform>();
		// Set size of inventory
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryW);
		inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryH);


		// Calculate number of columns
		int columns = slots / rows;

		// For each row
		for (int y = 0; y < rows; y++){

			// For each column
			for (int x = 0; x < columns; x++){
				// Instantiate a new slot object, cast as GO
				GameObject newSlot = (GameObject) Instantiate(slotPrefab);
				// Get a reference to new slot's transform
				RectTransform slotRect = newSlot.GetComponent<RectTransform>();

				// Rename the temp slot object
				newSlot.name = "Inventory_Slot";
				// Set the slot's parent to Canvas
				newSlot.transform.SetParent(this.transform.parent);
				// Set this slot's position
				slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),
				                                                                   -slotPaddingTop * (y + 1) - (slotSize * y));
				slotRect.localScale = new Vector3(slotSize * 0.0180f, slotSize * 0.0180f, slotSize * 0.0180f);
				// Set this slot's size
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

				// Append this slot to list of slots
				inventorySlots.Add(newSlot);

			}
		}

		// For every slot in the inventory
		for (int i = 0; i < inventorySlots.Count; i++){
			// As long as we haven't run out of items to add
			if (i < activeInventory.loItems.Count){
				// Create a temp item reference
				Item tmpItem = (Item) activeInventory.loItems[i];
				// Set this slot's source image to the item's sprite
				inventorySlots[i].GetComponent<Image>().sprite = tmpItem.getSprite();
				// Resize this slot's source image to 500x500 pixels
				inventorySlots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
			}
		}
	}
}
