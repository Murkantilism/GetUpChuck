using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraZoom : MonoBehaviour {

	private float zoomSpeed = 6.0f;
	private float zoomLimit = 3.0f;
	bool triggerZoom = false;
	
	// Update is called once per frame
	void Update () {
		if (triggerZoom == true){
			Zoom();
		}
	}

	public void TriggerZoom(){
		triggerZoom = true;
	}

	void Zoom(){
		if(this.camera.orthographicSize > zoomLimit){
			// Zoom camera in
			this.camera.orthographicSize -= Time.deltaTime * zoomSpeed;
			// Pan camera slightly to the right
			this.camera.transform.position = new Vector3(this.camera.transform.position.x - Time.deltaTime * zoomSpeed, 
			                                             this.camera.transform.position.y, 
			                                             this.camera.transform.position.z);
		}else{
			triggerZoom = false;
		}
	}
}