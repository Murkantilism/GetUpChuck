using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GameObject tutorialPanel;
	private Animator tutAnim;


	void Start () {
		tutAnim = tutorialPanel.GetComponent<Animator> ();
		tutAnim.enabled = false;
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
