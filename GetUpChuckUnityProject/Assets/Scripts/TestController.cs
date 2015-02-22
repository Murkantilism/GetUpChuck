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
		StartCoroutine(Wait(2));  // Wait 2 seconds while references elsewhere are set (Item_Combiner.cs)

		invnty_blue = GameObject.Find("Inventory").GetComponent<Inventory_Blue>();
		invnty_red = GameObject.Find("Inventory").GetComponent<Inventory_Red>();

		yellowTriangle = GameObject.Find("Item_yellowTriangle").GetComponent<Item>();
		redTriangle = GameObject.Find("Item_redTriangle").GetComponent<Item>();
		scrapMetal = GameObject.Find("Item_scrapMetal").GetComponent<Item>();
	}

	IEnumerator Wait(float waitTime)
	{
		if (waitTime > 0)
			yield return new WaitForSeconds(waitTime);
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

