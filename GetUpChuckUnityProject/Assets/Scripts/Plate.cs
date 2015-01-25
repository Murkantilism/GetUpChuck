using UnityEngine;
using System.Collections;

public class Plate : Obstacle, MonoBehaviour {

	public int weightCheck;
	bool heavyEnough = false;
	public Texture2D message;

	void conditionCheck(GameObject player) {
		//check if player's weight > weightCheck
	}

	void conditionMet() {
		// if the weight condition is true, trigger the mechanism for this plate
	}

	void shareMessage() {
		// display the player help message for this obstacle
	}
}
