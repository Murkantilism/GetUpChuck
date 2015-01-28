using UnityEngine;
using System.Collections;

public class TestCombiner : ScriptableObject {
	Inventory_Red redInventory;
	
	public ArrayList selectedItems = new ArrayList();
	
	public Item apple;
	public Item rock;
	public Item sugar;

	// Use this for initialization
	void Start () {
		apple = GameObject.Find("Apple").AddComponent<Item>();
		apple.setWeight(1.0f);
		apple.setHue("red");
		apple.setName("apple");
		apple.setDigestionTimer(5.0f);

		string [] appleCombinations = new string[2];
		appleCombinations[0] = "flour";
		appleCombinations[1] = "sugar";

		apple.setCombinations(appleCombinations);


		rock = GameObject.Find("Rock").AddComponent<Item>();
		rock.setWeight(2.0f);
		rock.setHue("blue");
		rock.setName("rock");
		rock.setDigestionTimer(1337.0f);
		
		string [] rockCombinations = new string[0];
		
		rock.setCombinations(rockCombinations);


		sugar = GameObject.Find("Sugar").AddComponent<Item>();
		sugar.setWeight(0.5f);
		sugar.setHue("red");
		sugar.setName("sugar");
		sugar.setDigestionTimer(3.0f);
		
		string [] sugarCombinations = new string[2];
		sugarCombinations[0] = "flour";
		sugarCombinations[1] = "apple";
		
		sugar.setCombinations(sugarCombinations);


		selectedItems.Add(sugar);
		selectedItems.Add(apple);
		selectedItems.Add(rock);
		
		redInventory = GameObject.Find("TestInventory").GetComponent<Inventory_Red>();
		
		redInventory.CheckItemCombinations(selectedItems);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
