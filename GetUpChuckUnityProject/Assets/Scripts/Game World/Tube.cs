using UnityEngine;
using System.Collections;

public class Tube : Obstacle, MonoBehaviour {

	public int weightCheck;
	bool fits = false;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") { condCheck(); }
	}

	void condCheck(Inventory inv) {
		if (inv.getCurrentWeight () < weightCheck) { condResponse(); } 
	}

	void condResponse() {
		fits = true;
	}

	public bool getStatus() {
		return fits;
	}
}
