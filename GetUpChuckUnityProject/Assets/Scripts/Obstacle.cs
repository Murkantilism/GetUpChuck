using UnityEngine;
using System.Collections;

public interface Obstacle : MonoBehaviour {

	// Check to see if the condition of this obstacle has been met
	void condCheck(GameObject check);

	// React when the condition has been met
	void condResponse();
}
