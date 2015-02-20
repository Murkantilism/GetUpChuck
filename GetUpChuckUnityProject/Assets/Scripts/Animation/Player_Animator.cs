using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GameObject))] // This script must be attached to a player gameobject
public class Player_Animator : MonoBehaviour {

	Animator anim; // The animator attached to player GO

	// Keep track of the state we are in, set by the MasterCtrl.cs script
	// 0 = idle, 1 = walk, 2 = jump, 3 = eat, 4 = vomit, 5 = sick
	public int animation_state = 0;

	// Hash string values for each animation
	//int idleHash = Animator.StringToHash("Base Layer.Chuck_Idle");
	//int walkHash = Animator.StringToHash("Base Layer.Chuck_Walk");
	//int jumpHash = Animator.StringToHash("Base Layer.Chuck_Jump");
	//int eatHash = Animator.StringToHash("Base Layer.Chuck_Eat");
	//int vomitHash = Animator.StringToHash("Base Layer.Chuck_Vomit");

	public SpriteRenderer spriteRenderer;
	Color sickColor = new Color(0.502f, 0.392f, 0.118f, 1.0f);
	Color healthyColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if(this.gameObject.name == "redPlayer"){
			spriteRenderer.color = Color.red;
		}else if(this.gameObject.name == "bluePlayer"){
			spriteRenderer.color = Color.blue;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.O)){
			animation_state = 1;
		}else if(Input.GetKey(KeyCode.P)){
			animation_state = 2;
		}else if(Input.GetKey(KeyCode.C)){
			animation_state = 3;
		}else if(Input.GetKey(KeyCode.V)){
			animation_state = 4;
		}else if(Input.GetKey(KeyCode.B)){
			animation_state = 5;
		}else{
			animation_state = 0;
		}

		// Get the current state of the animator
		// AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

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
	}

	public void Set_animation_state(int state){
		animation_state = state;
	}
}
