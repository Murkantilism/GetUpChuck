using UnityEngine;
using System.Collections;

public class Plate : Obstacle, MonoBehaviour {

	public int weightCheck;

	public void condCheck(Inventory inv) {
		if (inv.getCurrentWeight () > weightCheck) { condResponse(); } 
	}

	void condResponse() {
		// TODO if the weight condition is true, trigger the mechanism for this plate
	}
}
