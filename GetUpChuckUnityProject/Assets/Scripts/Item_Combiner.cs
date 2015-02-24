﻿using UnityEngine;
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
		redTriangle.setWeight(0.25f);
		redTriangle.setHue("red");
		redTriangle.setName("Item_redTriangle");
		redTriangle.setDigestionTimer(150.0f);

		ArrayList redTriangleCombinations = new ArrayList();
		redTriangleCombinations.Add(System.Tuple.Create("yellowTriangle"));
		redTriangle.setCombinations(redTriangleCombinations);
		loItems.Add(redTriangle);


		yellowTriangle = GameObject.Find("Item_yellowTriangle").AddComponent<Item>();
		yellowTriangle.setWeight(0.25f);
		yellowTriangle.setHue("red");
		yellowTriangle.setName("Item_yellowTriangle");
		yellowTriangle.setDigestionTimer(150.0f);

		ArrayList yellowTriangleCombinations = new ArrayList();
		yellowTriangleCombinations.Add(System.Tuple.Create("redTriangle"));
		yellowTriangle.setCombinations(yellowTriangleCombinations);
		loItems.Add(yellowTriangle);


		scrapMetal = GameObject.Find("Item_scrapMetal").AddComponent<Item>();
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