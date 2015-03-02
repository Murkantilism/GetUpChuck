using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GameObject pauseMenuPanel;
	public GameObject inventoryPanel;
	public GameObject tutorialPanel;
	private Animator pauseAnim;
	private Animator invAnim;
	private Animator tutAnim;
	private bool isPaused = false;

	void Start () {
		Time.timeScale = 1;
		pauseAnim = pauseMenuPanel.GetComponent<Animator>();
		invAnim = inventoryPanel.GetComponent<Animator>();
		tutAnim = tutorialPanel.GetComponent<Animator>();
		pauseAnim.enabled = false;
		invAnim.enabled = false;
		tutAnim.enabled = false;
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
	public void OpenInventory(){
		invAnim.enabled = true;
		invAnim.Play ("Inv_In");
	}
	public void CloseInventory(){
		invAnim.enabled = true;
		invAnim.Play ("Inv_Out");
	}
	public void tutorialTap(){
		tutAnim.enabled = true;
		tutAnim.Play ("Tut_Tap");
	}
	public void tutorialHold(){
		tutAnim.enabled = true;
		tutAnim.Play ("Tut_Hold");
	}
	public void tutorialSwipe(){
		tutAnim.enabled = true;
		tutAnim.Play ("Tut_Swipe");
	}
}
