using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item_Combiner : MonoBehaviour {
	public ArrayList selectedItems = new ArrayList();

	public Item redTriangle;
	public Item yellowTriangle;
	public Item scrapMetal;

	List<Item> loItems;

	// Use this for initialization
	void Awake () {
		loItems = new List<Item>();

		redTriangle = GameObject.Find("Item_redTriangle").AddComponent<Item>();
		redTriangle.gameObject.AddComponent<BoxCollider2D>();
		BoxCollider2D redBox = redTriangle.gameObject.GetComponent<BoxCollider2D>();
		redBox.size = new Vector2(2.0f, 2.0f);
		redBox.isTrigger = true;
		redTriangle.setWeight(0.25f);
		redTriangle.setHue("red");
		redTriangle.setName("Item_redTriangle");
		redTriangle.setDigestionTimer(150.0f);

		ArrayList redTriangleCombinations = new ArrayList();
		redTriangleCombinations.Add(System.Tuple.Create("yellowTriangle"));
		redTriangle.setCombinations(redTriangleCombinations);
		loItems.Add(redTriangle);


		yellowTriangle = GameObject.Find("Item_yellowTriangle").AddComponent<Item>();
		yellowTriangle.gameObject.AddComponent<BoxCollider2D>();
		BoxCollider2D yellowBox = yellowTriangle.gameObject.GetComponent<BoxCollider2D>();
		yellowBox.size = new Vector2(2.0f, 2.0f);
		yellowBox.isTrigger = true;
		yellowTriangle.setWeight(0.25f);
		yellowTriangle.setHue("red");
		yellowTriangle.setName("Item_yellowTriangle");
		yellowTriangle.setDigestionTimer(150.0f);

		ArrayList yellowTriangleCombinations = new ArrayList();
		yellowTriangleCombinations.Add(System.Tuple.Create("redTriangle"));
		yellowTriangle.setCombinations(yellowTriangleCombinations);
		loItems.Add(yellowTriangle);


		scrapMetal = GameObject.Find("Item_scrapMetal").AddComponent<Item>();
		scrapMetal.gameObject.AddComponent<BoxCollider2D>();
		BoxCollider2D scrapBox = scrapMetal.gameObject.GetComponent<BoxCollider2D>();
		scrapBox.size = new Vector2(2.0f, 2.0f);
		scrapBox.isTrigger = true;
		scrapMetal.setWeight(2.0f);
		scrapMetal.setHue("red");
		scrapMetal.setName("Item_scrapMetal");
		scrapMetal.setDigestionTimer(300.0f);

		// No items can be combined with scrapMetal
		ArrayList scrapMetalCombinations = new ArrayList();
		scrapMetal.setCombinations(scrapMetalCombinations);
		loItems.Add(scrapMetal);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
