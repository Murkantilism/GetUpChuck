using UnityEngine;
using System.Collections;

public class Pit : Obstacle, MonoBehaviour {

	bool inPit = false;

	public void condCheck(GameObject player) {
		// TODO check if player is in pit
	}

	void condResponse() {
		// TODO if (inPit) -> kill player
	}
}
