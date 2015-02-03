using UnityEngine;
using System.Collections;

public class Tube : Obstacle, MonoBehaviour {

	public int weightCheck;
	bool fits = false;
	Inventory inv;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Player")) { 
			if (inv.getCurrentWeight () < weightCheck) { condResponse(); } 
		}
	}

	void condResponse() {
		fits = true;
	}

	public void setInventory(Inventory inv) {
		this.inv = inv;
	}

	public bool getStatus() {
		return fits;
	}
}