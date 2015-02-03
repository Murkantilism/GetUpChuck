using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour, Obstacle {

	public int weightCheck;
	bool pressed = false;
	Inventory inv;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Player")) { 
			if (inv.getCurrentWeight () > weightCheck) { condResponse(); }
		}
	}

	void condResponse() {
		pressed = true;
	}

	public void setInventory(Inventory inv) {
		this.inv = inv;
	}

	public bool getStatus() {
		return pressed;
	}
}
