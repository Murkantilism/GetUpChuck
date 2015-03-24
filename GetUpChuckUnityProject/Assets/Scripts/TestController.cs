using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {

	int activePlayer = 0; // Red by default

	Inventory_Blue invnty_blue;
	Inventory_Red invnty_red;

	public Item redTriangle;
	public Item yellowTriangle;
	public Item scrapMetal;
	
	// Use this for initialization
	void Start () {
		invnty_blue = GameObject.FindGameObjectWithTag("blue").GetComponent<Inventory_Blue>();
		invnty_red = GameObject.FindGameObjectWithTag("red").GetComponent<Inventory_Red>();

		yellowTriangle = GameObject.Find("Item_yellowTriangle").GetComponent<Item>();
		redTriangle = GameObject.Find("Item_redTriangle").GetComponent<Item>();
		scrapMetal = GameObject.Find("Item_scrapMetal").GetComponent<Item>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.F)){
			FakeItemTap(redTriangle);
		}
		if(Input.GetKeyUp(KeyCode.G)){
			FakePlayerTap(redTriangle);
		}
	}

	// This function will call stuff as if user had tapped an object and was within range of it.
	void FakeItemTap(Item item){
		if(activePlayer == 0){
			item.AddToInventory(invnty_red);
		}else{
			item.AddToInventory(invnty_blue);
		}
	}
	// This function will call stuff as if user had tapped player to throw up.
	void FakePlayerTap(Item item){
		if(activePlayer == 0){
			invnty_red.DropItem(item);
		}else{
			invnty_blue.DropItem(item);
		}
	}
}

