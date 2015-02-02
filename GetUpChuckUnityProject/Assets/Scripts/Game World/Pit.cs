using UnityEngine;
using System.Collections;

public class Pit : Obstacle, MonoBehaviour {

	bool inPit = false;
	Inventory inv;
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Player")) { condResponse(); }
	}

	void condResponse() {
		inPit = true;
	}

	public bool getStatus() {
		return inPit;
	}
}
