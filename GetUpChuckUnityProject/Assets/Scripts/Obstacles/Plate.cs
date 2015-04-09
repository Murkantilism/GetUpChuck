using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour, Obstacle {

	public int weightCheck;
	public Gate target;
	private HUD hud;
	private Animator anim;
	private bool pressed = false;
	private Inventory inv;
	public GameObject objectWithGate;

	void Start(){
		hud = GameObject.Find("UI_Handler").GetComponent<HUD>();
		anim = this.gameObject.GetComponent<Animator> ();

		target = objectWithGate.GetComponent<Gate> ();
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
		if (inv.getCurrentWeight () >= weightCheck) {
			pressed = true;
			anim.Play("Button_Press");
			target.condResponse ();
		} else {
			hud.TooSmall ();
		}
	}

	public bool getStatus() {
		return pressed;
	}
}
