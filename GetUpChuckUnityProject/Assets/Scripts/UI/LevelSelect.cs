using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

	private GameObject level;
	private GameObject lockBox;
	private Animator anim;
	private int levelReached;

	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		anim.Play ("Show_Levels");
		levelReached = PlayerPrefs.GetInt("level") + 1;
		UnlockLevels ();
	}

	void UnlockLevels() {
		for (int i=2; i <= levelReached; i++) {
			level = GameObject.Find("Lvl" + i);
			level.GetComponent<Button>().interactable = true;
			lockBox = level.transform.GetChild (0).gameObject;
			GameObject.Destroy (lockBox);
		}
	}
}
