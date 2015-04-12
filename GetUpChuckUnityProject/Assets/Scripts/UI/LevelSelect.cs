using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	public int levelReached;
	private GameObject level;
	private GameObject lockBox;
	private Button butt;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		anim.Play ("Show_Levels");
		UnlockLevels ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UnlockLevels() {
		for (int i=1; i<= levelReached; i++) {
			level = GameObject.Find("Lvl" + i.ToString());
			level.GetComponent<Button>().interactable = true;
			lockBox = level.transform.GetChild (0).gameObject;
			GameObject.Destroy (lockBox);
		}
	}
}
