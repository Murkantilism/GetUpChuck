using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraZoomOut : MonoBehaviour {
	
	private float zoomSpeed = 6.0f;
	private float zoomOutLimit = 10.0f;
	public bool triggerZoom = false;
	CameraZoom cameraZoom;

	void Start () {
		cameraZoom = this.gameObject.GetComponent<CameraZoom>();
	}

	// Update is called once per frame
	void Update () {
		if (triggerZoom == true){
			Zoom();
		}
	}
	
	public void SetZoomState(bool b){
		// If we camera isn't already zooming in, set zoom state to given bool
		if (cameraZoom.GetZoomState() == false){
			triggerZoom = b;
		}
	}

	public bool GetZoomState(){
		return triggerZoom;
	}

	// Zoom out the camera
	void Zoom(){
		if(this.camera.orthographicSize < zoomOutLimit){
			this.camera.orthographicSize += Time.deltaTime * zoomSpeed;
		}else{
			triggerZoom = false;
		}
	}
}
