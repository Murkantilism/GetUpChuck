using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool isKeyItem = false;

	//name for key items for ID
	public string keyName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//destroy item when eaten
	public void eatMe(){
		Destroy (this.gameObject);
	}
}
