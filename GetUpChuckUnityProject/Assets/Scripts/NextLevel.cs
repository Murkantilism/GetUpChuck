using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	//name of next level
	public string NextLevelName;
	public int thisLevelNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void goToNextLevel(){
		PlayerPrefs.SetInt ("level", thisLevelNumber);
		PlayerPrefs.Save();
		Application.LoadLevel (NextLevelName);
	}

}
