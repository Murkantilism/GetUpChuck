using UnityEngine;
using System.Collections;

public class Plate : Obstacle, MonoBehaviour {

	public int weightCheck;
	bool heavyEnough = false;

	void condCheck(Inventory inv) {
		if (inv.getCurrentWeight () > weightCheck) {
			heavyEnough = true;
		} else {
			heavyEnough = false;
		}
	}

	void condResponse() {
		// TODO if the weight condition is true, trigger the mechanism for this plate
	}
}
