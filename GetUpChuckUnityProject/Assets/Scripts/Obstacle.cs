using UnityEngine;
using System.Collections;

public interface Obstacle : MonoBehaviour {

	// Check to see if the condition of this obstacle has been met
	void conditionCheck(GameObject check);

	// React when the condition has been met
	void conditionMet();

	// Display the help message for this obstacle to the player
	void shareMessage();
}
