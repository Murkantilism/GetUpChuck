using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour, Obstacle {

	public bool inPit = false;
	public GameObject spawnPoint;
	GameObject chuck;

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("red") || coll.gameObject.tag.Equals("blue")) { 
			chuck = coll.gameObject;
			condResponse(); 
		}
	}

	public void condResponse() {
		inPit = true;
		chuck.GetComponent<Transform>().position = spawnPoint.GetComponent<Transform>().position;
	}

	public bool getStatus() {
		return inPit;
	}
}
