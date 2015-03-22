using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GameObject))] // This script must be attached to a player gameobject
public class Player_Animator : MonoBehaviour {

	protected Animator anim; // The animator attached to player GO

	Vector3 lastPosn; //last position used for movement animation

	// Keep track of the state we are in, set by the MasterCtrl.cs script
	// 0 = idle, 1 = walk, 2 = jump, 3 = eat, 4 = vomit, 5 = sick
	public int animation_state = 0;

	public SpriteRenderer spriteRenderer;
	Color sickColor = new Color(0.502f, 0.392f, 0.118f, 1.0f);
	Color healthyColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if(this.gameObject.name == "redPlayer"){
			spriteRenderer.color = new Color(200.0f/255.0f, 35.0f/255.0f, 35.0f/255.0f, 1.0f);//Color.red;
		}else if(this.gameObject.name == "bluePlayer"){
			spriteRenderer.color = new Color(35.0f/255.0f, 92.0f/255.0f, 205.0f/255.0f, 1.0f);//Color.blue;
		}
		lastPosn = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// If animation state is 0, 1, or 2
		if (animation_state < 3){
			// Set the movement int to animatio_state value
			anim.SetInteger("Movement", animation_state);
			anim.SetBool("isEating", false);
			anim.SetBool("isVomiting", false);
		// Otherwise set boolean triggers based on state value
		}else if (animation_state == 3){
			anim.SetBool("isEating", true);
		}else if (animation_state == 4){
			anim.SetBool("isVomiting", true);
		}else if (animation_state == 5){
			spriteRenderer.color = sickColor;
			Debug.Log(spriteRenderer.color);
			anim.SetBool("isSick", true);
		} 

		animation_state = 0;

	}

	//setter for animation state
	public void Set_animation_state(int state){
		animation_state = state;
	}
}
