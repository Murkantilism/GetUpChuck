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
		if (load) {
			Application.LoadLevel("devScene");
		}
	}

	public void LoadLevel() {
		anim.enabled = true;
		anim.Play("LoadPanel");		
	}
}