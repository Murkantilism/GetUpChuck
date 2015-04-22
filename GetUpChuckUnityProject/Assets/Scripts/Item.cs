using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isKeyItem = false;

	//is this the cake that leads to the next level
	public bool isTheCake = false;
	NextLevel nextLvForCake;
	//sound effect for level end for cake
	AudioSource cakeWin;

	//name for key items for ID
	public string keyName;

	// Use this for initialization
	void Start () {
		if (isTheCake) {
			nextLvForCake = this.GetComponent<NextLevel>();
			cakeWin = this.GetComponent<AudioSource>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//destroy item when eaten
	public void eatMe(){
		if (isTheCake) {
						StartCoroutine (eatMeCake());
				} else {
						Destroy (this.gameObject);
				}
	}

	public IEnumerator eatMeCake(){
		//TODO uncomment this when win sound is attached
		//cakeWin.Play();
		yield return new WaitForSeconds(1);
		nextLvForCake.goToNextLevel();
	}
}
