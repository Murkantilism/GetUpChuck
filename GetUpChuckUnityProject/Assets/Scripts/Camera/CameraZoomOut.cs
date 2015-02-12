using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraZoomOut : MonoBehaviour {
	
	private float zoomSpeed = 6.0f;
	private float zoomOutLimit = 10.0f;
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
		if(this.camera.orthographicSize < zoomOutLimit){
			this.camera.orthographicSize += Time.deltaTime * zoomSpeed;
		}else{
			triggerZoom = false;
		}
	}
}
