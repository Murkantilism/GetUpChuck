using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour, Obstacle {

	public int weightCheck;
	public Gate target;
	private HUD hud;
	private Animator anim;
	private bool pressed = false;
	private Inventory redInv;
	private Inventory blueInv;
	public GameObject somethingWithAGate;
	private Inventory tmpI;


	void Start(){
		hud = GameObject.Find("UI_Handler").GetComponent<HUD>();
		anim = this.gameObject.GetComponent<Animator> ();
		target = somethingWithAGate.GetComponent<Gate> ();

		redInv = GameObject.Find ("redPlayer").GetComponent<Inventory_Red> ();
		blueInv = GameObject.Find("bluePlayer").GetComponent<Inventory_Blue>();
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		// On collision with either player character, get that character's inventory. On other collisions, terminate.
		if (coll.gameObject.tag.Equals ("red")) {
			condResponse("red");
		} else if (coll.gameObject.tag.Equals ("blue")) {
			condResponse("blue");
		} else {
			Debug.Log("not red or blue");
			return;
		}		
	}

	public void condResponse(){

		}

	public void condResponse(string colr) {
		if(colr.Equals("red")){
			tmpI = redInv;
		}
		if(colr.Equals("blue")){
			tmpI = blueInv;
		}
		if (tmpI.getCurrentWeight () >= weightCheck) {
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
