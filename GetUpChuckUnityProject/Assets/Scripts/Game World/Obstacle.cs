using UnityEngine;
using System.Collections;

public interface Obstacle {

	// Check for player collisions and any further conditions to trigger this obstacle
	void OnCollisionEnter2D(Collision2D coll);

	// React when all conditions have been met
	void condResponse();

	// Getter for whether the obstacle has been opened/triggered
	bool getStatus();
}
