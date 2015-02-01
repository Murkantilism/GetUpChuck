using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {

	int activePlayer = 0; // Red by default

	Inventory_Blue invnty_blue;
	Inventory_Red invnty_red;

	public Item apple;
	
	// Use this for initialization
	void Start () {
		invnty_blue = GameObject.Find("Inventory").GetComponent<Inventory_Blue>();
		invnty_red = GameObject.Find("Inventory").GetComponent<Inventory_Red>();

		apple = GameObject.Find("Apple").AddComponent<Item>();
		apple.setWeight(1.0f);
		apple.setHue("red");
		apple.setName("apple");
		apple.setDigestionTimer(5.0f);
		
		ArrayList appleCombinations = new ArrayList();
		appleCombinations.Add(System.Tuple.Create("dough", "applepie"));
		
		apple.setCombinations(appleCombinations);

	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.F)){
			FakeItemTap(apple);
		}
		if(Input.GetKeyUp(KeyCode.D)){
			FakePlayerTap(apple);
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

