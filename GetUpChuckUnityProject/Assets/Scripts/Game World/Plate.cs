﻿using UnityEngine;
using System.Collections;

public class Plate : Obstacle, MonoBehaviour {

	public int weightCheck;
	bool pressed = false;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { condCheck(); }
	}

	void condCheck(Inventory inv) {
		if (inv.getCurrentWeight () > weightCheck) { condResponse(); } 
	}

	void condResponse() {
		pressed = true;
	}

	public bool getStatus() {
		return pressed;
	}
}
