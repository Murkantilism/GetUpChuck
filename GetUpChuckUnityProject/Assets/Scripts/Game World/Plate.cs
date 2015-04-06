using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour, Obstacle {

	public int weightCheck;
	public Door target;
	bool pressed = false;
	Inventory inv;

	public void OnCollisionEnter2D(Collision2D coll) {
		// On collision with either player character, get that character's inventory. On other collisions, terminate.
		if (coll.gameObject.tag.Equals ("red")) { 
			inv = coll.gameObject.GetComponent<Inventory_Red> ();
		} else if (coll.gameObject.tag.Equals ("blue")) {
			inv = coll.gameObject.GetComponent<Inventory_Blue> ();
		} else {
			return;
		}

		if (inv.getCurrentWeight() >= weightCheck) { condResponse(); }
	}

	public void condResponse() {
		pressed = true;
		target.condResponse();
	}

	public bool getStatus() {
		return pressed;
	}
}
