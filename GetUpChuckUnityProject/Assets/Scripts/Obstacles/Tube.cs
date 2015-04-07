using UnityEngine;
using System.Collections;

public class Tube : MonoBehaviour, Obstacle {

	public int weightCheck;
	private HUD hud;
	private bool fits = false;
	private Inventory inv;

	private Inventory redInv;
	private Inventory blueInv;
	private Inventory tmpI;

	void Start(){
		hud = GameObject.Find("UI_Handler").GetComponent<HUD>();

		redInv = GameObject.Find ("redPlayer").GetComponent<Inventory_Red> ();
		blueInv = GameObject.Find("bluePlayer").GetComponent<Inventory_Blue>();
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		// On collision with either player character, get that character's inventory. On other collisions, terminate.
		if (coll.gameObject.tag.Equals ("red")) { 
			tmpI = redInv;
		} else if (coll.gameObject.tag.Equals ("blue")) {
			tmpI = blueInv;
		} else {
			return;
		}
		condResponse();  
	}

	public void condResponse() {
		if (tmpI.getCurrentWeight () <= weightCheck) {
			fits = true;
			this.GetComponent<BoxCollider2D> ().isTrigger = true;
		} else {
			hud.TooBig ();
		}
	}

	public bool getStatus() {
		return fits;
	}
}