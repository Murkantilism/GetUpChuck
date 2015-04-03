using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	
	public GameObject pauseMenuPanel;
	public GameObject inventoryPanel;
	public GameObject mainPanel;
	private Animator pauseAnim;
	private Animator invAnim;
	private Animator mainAnim;
	private bool isPaused = false;

	void Start () {
		Time.timeScale = 1;
		pauseAnim = pauseMenuPanel.GetComponent<Animator>();
		invAnim = inventoryPanel.GetComponent<Animator>();
		mainAnim = mainPanel.GetComponent<Animator>();
		//mainAnim.enabled = false;
		pauseAnim.enabled = false;
		invAnim.enabled = false;
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
		pauseAnim.enabled = true;
		pauseAnim.Play ("Pause_In");
		isPaused = true;
		Debug.Log("paused");
		//Time.timeScale = 0;
	}
	public void UnpauseGame(){
		isPaused = false;
		pauseAnim.Play ("Pause_Out");
		Time.timeScale = 1;
	}

	public void TooSmall() {
		mainAnim.Play("Too_Small");
	}
	public void TooBig() {
		mainAnim.Play("Too_Big");
	}
	public void ItemPickup() {
		mainAnim.Play ("Key_Pickup");
	}

	// These inventory methods may no longer be needed
	public void OpenInventory(){
		invAnim.enabled = true;
		invAnim.Play ("Inv_In");
	}
	public void CloseInventory(){
		invAnim.enabled = true;
		invAnim.Play ("Inv_Out");
	}
}

