using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private Animator anim;
	public bool load = false;
	
	void Start() {
		anim = this.GetComponent<Animator>();
		anim.enabled = false;
	}

	void Update() {
		// Public load field is false by default, LoadPanel animation
		// sets it to true when the animation is complete and the level
		// is ready to be loaded
		if (load) {
			Application.LoadLevel("LevelSelect");
		}
	}

	public void LoadLevel() {
		anim.enabled = true;
		anim.Play("Load_Panel");		
	}
}

