using UnityEngine;
using System.Collections;

public class Door : Obstacle, MonoBehaviour {

	bool open = false;
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Key") { condCheck(); }
	}

	void condCheck(Inventory inv) {
		// OnCollisionEnter2D satisfies the condition for this obstacle
		// Follow through to response
		condResponse();
	}
	
	void condResponse() {
		open = true;
	}

	public bool getStatus() {
		return open;
	}
}
