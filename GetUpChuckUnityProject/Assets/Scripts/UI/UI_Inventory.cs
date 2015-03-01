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

	// Use this for initialization
	void Start () {
		CreateInventory();

		blue_inventory = GameObject.FindGameObjectWithTag("blue").GetComponent<Inventory_Blue>();
		red_inventory = GameObject.FindGameObjectWithTag("red").GetComponent<Inventory_Red>();

		for (int i = 0; i < blue_inventory.loItems.Count; i++){
			Debug.Log(blue_inventory.loItems[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Setup the initial inventory
	private void CreateInventory(){
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

				// TODO: Set this slot's source image to the item's sprite
				//newSlot.GetComponent<Image>() = ;

				// Append this slot to list of slots
				inventorySlots.Add(newSlot);

			}
		}

	}
}
