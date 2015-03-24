using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraZoomOut : MonoBehaviour {
	
	private float zoomSpeed = 6.0f;
	private float zoomOutLimit = 14.0f;
	float heighAntiDamping = 0.0f;
	public bool triggerZoom = false;
	CameraZoom cameraZoom;
	CameraSmoothFollow smoothFollow;

	void Start () {
		cameraZoom = this.gameObject.GetComponent<CameraZoom>();
		smoothFollow = this.GetComponent<CameraSmoothFollow>();
		heighAntiDamping = cameraZoom.heightDamping;
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
			smoothFollow.height += Time.deltaTime * heighAntiDamping;
		}else{
			triggerZoom = false;
		}
	}
}
