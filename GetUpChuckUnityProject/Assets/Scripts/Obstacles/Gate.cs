using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour, Obstacle {

	public bool open = false;
	private Animator anim;

	void Start() {
		anim = this.gameObject.GetComponent<Animator> ();
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if (this.gameObject.tag.Equals ("Door")) {
			if (coll.gameObject.tag.Equals ("Key")) {
				condResponse ();
			}
		}
	}	
	public void condResponse() {
		open = true;
		if (this.gameObject.tag.Equals ("Door")) {
			//anim.Play ("Door_Open");
			BoxCollider2D doorCol = this.GetComponent<BoxCollider2D>();
			doorCol.isTrigger = true;
		} else if (this.gameObject.tag.Equals ("Pipe")) {
			//anim.Play ("Pipe_Open");
			BoxCollider2D doorCol = this.GetComponent<BoxCollider2D>();
			doorCol.isTrigger = true;
		}
	}
	public bool getStatus() {
		return open;
	}
}
