using UnityEngine;
using System.Collections;

public class Tube : MonoBehaviour, Obstacle {

	public int weightCheck;
	private HUD hud;
	private bool fits = false;
	private Inventory inv;

	void Start(){
		hud = GameObject.Find("UI_Handler").GetComponent<HUD>();
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		// On collision with either player character, get that character's inventory. On other collisions, terminate.
		if (coll.gameObject.tag.Equals ("red")) { 
			inv = GameObject.Find("redPlayer").GetComponent<Inventory_Red> ();

		} else if (coll.gameObject.tag.Equals ("blue")) {
			inv = GameObject.Find("bluePlayer").GetComponent<Inventory_Blue> ();
		} else {
			return;
		}
		condResponse();  
	}

	public void condResponse() {
		if (inv.getCurrentWeight () < weightCheck) {
			fits = true;
			this.GetComponent<BoxCollider2D> ().isTrigger = true;
		} else {
			hud.TooBig ();
		}
	}

	public void setInventory(Inventory inv) {
		this.inv = inv;
	}

	public bool getStatus() {
		return fits;
	}
}