using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour, Obstacle {

	public bool inPit = false;

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("red") || coll.gameObject.tag.Equals("blue")) {
			condResponse();
		}
	}

	public void condResponse() {
		inPit = true;
	}

	public bool getStatus() {
		return inPit;
	}
}
