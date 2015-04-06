using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, Obstacle {

	public bool open = false;

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("Key")) { condResponse(); }
	}
	
	public void condResponse() {
		open = true;
		this.GetComponent<BoxCollider2D>().isTrigger = true;
	}

	public bool getStatus() {
		return open;
	}
}
