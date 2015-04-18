using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	//from sams branch added manualy
	public Sprite blank;
	private GameObject canvas;
	private Animator mainAnim;

	//public GameObject pauseMenuPanel;
	//private Animator pauseAnim;
	private bool isPaused = false;
	//UI_Inventory inventory;
	MasterCtrl master;

	void Start () {
		Time.timeScale = 1;
		//pauseAnim = pauseMenuPanel.GetComponent<Animator>();
		//pauseAnim.enabled = false;
		master = GameObject.Find("MasterController").GetComponent<MasterCtrl>();

		//from sams branch added manualy
		canvas = GameObject.Find("Canvas");
		mainAnim = canvas.GetComponent<Animator>();
		blank = Resources.Load<Sprite>("blank");
	}
	
	public void Update () {
		if(Input.GetKeyUp(KeyCode.Escape) && !isPaused){
			PauseGame();
		}
		else if(Input.GetKeyUp(KeyCode.Escape) && isPaused){
			UnpauseGame();
		}
	}
	public void PauseGame(){
		//pauseAnim.enabled = true;
		mainAnim.Play ("Pause_In");
		isPaused = true;
		Debug.Log("paused");
		//Time.timeScale = 0;
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

	public void SwapPlayers(){
		master.swapPlayer();
		Debug.Log("Player Swap");
	}
	public void RespawnPlayer(){
		master.activePlayer.playerRe();
	}
}