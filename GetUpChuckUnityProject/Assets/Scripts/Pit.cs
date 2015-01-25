using UnityEngine;
using System.Collections;

public class Pit : Obstacle, MonoBehaviour {

	bool inPit = false;
	public Texture2D message;

	void conditionCheck(GameObject player) {
		// check if player is in pit
	}

	void conditionMet() {
		// if (inPit) -> kill player
	}

	void shareMessage() {
		// display message texture somewhere on screen
	}

}
