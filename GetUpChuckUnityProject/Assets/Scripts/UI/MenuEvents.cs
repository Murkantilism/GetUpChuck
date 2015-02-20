using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour {

	public void LoadLevel(int levelIndex) {
		Application.LoadLevel(levelIndex);
	}
	
	public void LoadLevel(string levelName) {
		Application.LoadLevel(levelName);
	}
	
	public void ExitApplication() {
		Application.Quit();
	}
}