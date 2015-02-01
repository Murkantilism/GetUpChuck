﻿using UnityEngine;
using System.Collections;

public class Pit : Obstacle, MonoBehaviour {

	bool inPit = false;
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { condCheck(); }
	}
	
	void condCheck(Inventory inv) {
		// OnCollisionEnter2D satisfies the condition for this obstacle
		// Follow through to response
		condResponse();
	}
	
	void condResponse() {
		inPit = true;
	}
	
	public bool getStatus() {
		return inPit;
	}
}
