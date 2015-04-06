using UnityEngine;
using System.Collections;

public class SaveProg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//saves the fact that a player has reached a certain level
	//TODO call at start of each level
	public void saveLevel(int levNum){
		PlayerPrefs.SetInt ("level", levNum);
		PlayerPrefs.Save();
		}

	//returns the max level reached so it can be loaded from the level select screen
	public int loadLevel(){
		if (PlayerPrefs.HasKey ("level")) {
						return PlayerPrefs.GetInt ("level");
				} else {
						return 1;
				}
	}
}
