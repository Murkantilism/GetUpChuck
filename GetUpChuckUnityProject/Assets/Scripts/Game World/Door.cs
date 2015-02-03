using UnityEngine;
using System.Collections;

public class Door : Obstacle, MonoBehaviour {

	bool open = false;
	Inventory inv;
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Key")) { condResponse(); }
	}
	
	void condResponse() {
		open = true;
	}

	public bool getStatus() {
		return this.open;
	}
}
