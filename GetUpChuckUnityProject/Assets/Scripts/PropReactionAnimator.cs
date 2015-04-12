using UnityEngine;
using System.Collections;

public class PropReactionAnimator : MonoBehaviour {

	public Sprite normal;
	public Sprite reaction;
	public float reactionTimer = 1.5f;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = normal;
	}

	// When the BroadCast Message goes out, switch the sprite to the reactionary sprite
	void PropReact(){
		this.GetComponent<SpriteRenderer>().sprite = reaction;
		StartCoroutine("BackToNormal");
	}

	// After reaction timer expires set sprite back to normal
	IEnumerator BackToNormal(){
		yield return new WaitForSeconds(reactionTimer);
		this.GetComponent<SpriteRenderer>().sprite = normal;
	}
}
