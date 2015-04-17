using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GameObject pauseMenuPanel;
	public GameObject inventoryPanel;
	private Animator pauseAnim;
	private Animator invAnim;
	private bool isPaused = false;
	UI_Inventory inventory;
	MasterCtrl master;

	void Start () {
		inventory = GameObject.Find("InventoryPanel").GetComponent<UI_Inventory>();
		Time.timeScale = 1;
		pauseAnim = pauseMenuPanel.GetComponent<Animator>();
		invAnim = inventoryPanel.GetComponent<Animator>();
		pauseAnim.enabled = false;
		invAnim.enabled = false;
		master = GameObject.Find("MasterController").GetComponent<MasterCtrl>();
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
		pauseAnim.Play ("PauseMenuSlideIn");
		isPaused = true;
		Debug.Log("paused");
		//Time.timeScale = 0;
	}
	public void UnpauseGame(){
		isPaused = false;
		pauseAnim.Play ("PauseMenuSlideOut");
		Time.timeScale = 1;
	}
	public void OpenInventory(){
		invAnim.enabled = true;
		invAnim.Play ("InventoryFadeIn");
	}
	public void CloseInventory(){
		invAnim.enabled = true;
		invAnim.Play ("InventoryFadeOut");
	}
	public void SwapPlayers(){
		master.swapPlayer();
		Debug.Log("Player Swap");
	}
	public void RespawnPlayer(){
		master.activePlayer.playerRe();
	}
}