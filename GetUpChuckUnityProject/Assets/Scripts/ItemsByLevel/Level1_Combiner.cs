using UnityEngine;
using System.Collections;

public class Level1_Combiner : ScriptableObject {
	Inventory_Red redInventory;
	
	public ArrayList selectedItems = new ArrayList();

	public Item red_triangle;
	public Item yellow_triangle;
	public Item scrapMetal;

	// Use this for initialization
	void Start () {
		red_triangle = GameObject.Find("Item_triangle").AddComponent<Item>();
		red_triangle.setWeight(0.25f);
		red_triangle.setHue("red");
		red_triangle.setName("red_triangle");
		red_triangle.setDigestionTimer(150.0f);

		ArrayList red_triangleCombinations = new ArrayList();
		red_triangleCombinations.Add(System.Tuple.Create("yellow_triangle"));
		red_triangle.setCombinations(red_triangleCombinations);


		yellow_triangle = GameObject.Find("Item_triangle").AddComponent<Item>();
		yellow_triangle.setWeight(0.25f);
		yellow_triangle.setHue("red");
		yellow_triangle.setName("yellow_triangle");
		yellow_triangle.setDigestionTimer(150.0f);

		ArrayList yellow_triangleCombinations = new ArrayList();
		yellow_triangleCombinations.Add(System.Tuple.Create("red_triangle"));
		yellow_triangle.setCombinations(yellow_triangleCombinations);


		scrapMetal = GameObject.Find("Item_scrapMetal").AddComponent<Item>();
		scrapMetal.setWeight(2.0f);
		scrapMetal.setHue("red");
		scrapMetal.setName("scrapMetal");
		scrapMetal.setDigestionTimer(300.0f);

		// No items can be combined with scrapMetal
		ArrayList scrapMetalCombinations = new ArrayList();
		scrapMetal.setCombinations(scrapMetalCombinations);

		/*
		flour = GameObject.Find("Flour").AddComponent<Item>();
		flour.setWeight(0.5f);
		flour.setHue("red");
		flour.setName("flour");
		flour.setDigestionTimer(3.0f);
		
		ArrayList flourCombinations = new ArrayList();
		flourCombinations.Add(System.Tuple.Create("sugar", "dough"));
		flourCombinations.Add(System.Tuple.Create("water", "mush"));
		flourCombinations.Add(System.Tuple.Create("flower", "pun"));
		
		flour.setCombinations(flourCombinations);
		
		// ADD ITEMS TO FAKE "SELECTED ITEMS LIST"
		selectedItems.Add(sugar);
		selectedItems.Add(flour);
		//selectedItems.Add(rock);
		//selectedItems.Add(apple);
		
		// CALL INVENTORY CLASS TO TEST COMBINATION CHECKER
		redInventory = GameObject.Find("Inventory").GetComponent<Inventory_Red>();
		redInventory.CheckItemCombinations(selectedItems);
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
