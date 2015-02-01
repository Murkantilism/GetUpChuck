using UnityEngine;
using System.Collections;

public interface Obstacle : MonoBehaviour {

	// Check for collisions with Player or other game objects
	void OnCollisionEnter2D(Collision2D coll);

	// Check to see if the condition of this obstacle has been met
	void condCheck(GameObject check);

	// React when the condition has been met
	void condResponse();

	// Public getter for whether the obstacle has been opened/triggered
	bool getStatus();
}
