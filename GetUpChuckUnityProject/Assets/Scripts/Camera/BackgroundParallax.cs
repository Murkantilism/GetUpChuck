using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	private float camPos;
	public float speed;
	// Follow Camera bool: 
	// If the element is a background that should move constantly, set to true
	// If the element is a foreground that should not move, set to false
	public bool followCam;
	Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		camPos = mainCamera.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(followCam == true){
			transform.position = new Vector3((mainCamera.transform.position.x - camPos) / speed, transform.position.y, transform.position.z);
		}else{
			transform.position = new Vector3((camPos - mainCamera.transform.position.x) / speed, transform.position.y, transform.position.z);
		}
	}
}
