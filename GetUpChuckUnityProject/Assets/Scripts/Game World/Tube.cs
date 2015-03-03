using UnityEngine;
using System.Collections;

public class Tube : MonoBehaviour, Obstacle {

	public int weightCheck;
	bool fits = false;
	Inventory inv;

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("red") || coll.gameObject.tag.Equals("blue")) {
			if (inv.getCurrentWeight () < weightCheck) {
				condResponse();
			} 
		}
	}

	public void condResponse() {
		fits = true;
	}

	public void setInventory(Inventory inv) {
		this.inv = inv;
	}

	public bool getStatus() {
		return fits;
	}
}