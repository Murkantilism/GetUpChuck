using UnityEngine;
using System.Collections;

public interface NPC {

	// Communicate this NPC's message to the player
	void speak();

	// Check if this NPC's condition has been met
	void condCheck();

	// React if this NPC's condition has been met
	void condResponse();
}
