using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	
	public Sprite blank;
	private GameObject canvas;
	private Animator mainAnim;
	private bool isPaused = false;

	void Start () {
		canvas = GameObject.Find("Canvas");
		mainAnim = canvas.GetComponent<Animator>();
		blank = Resources.Load<Sprite>("blank");
	}
	
	public void Update () { }

	public void PauseGame(){
		mainAnim.Play ("Pause_In");
		isPaused = true;
	}
	public void UnpauseGame(){
		isPaused = false;
		mainAnim.Play ("Pause_Out");
		Time.timeScale = 1;
	}
	public void ItemPickup() {
		mainAnim.Play ("Key_Pickup");
	}
	public void ItemDiscard() {
		mainAnim.Play("Key_Discard");
	}
	// Messages for when the character is too big or too small for an obstacle
	public void TooSmall() {
		mainAnim.Play("Too_Small");
	}
	public void TooBig() {
		mainAnim.Play("Too_Big");
	}
}

