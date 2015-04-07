using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraPan : MonoBehaviour {

	float panSpeed;
	float playerDistance;

	private float currentXpos;
	private float desiredXpos;
	private float direction;
	private float panSpeedRatio = 0.1f; // returns 10% of distance as pan speed
	private float cameraPanBuffer = 2.0f;

	bool triggerPan = false;

	GameObject activePlayer_go;
	GameObject inactivePlayer_go;

	CameraSmoothFollow smoothFollow;

	// Use this for initialization
	void Start () {
		smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSmoothFollow>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate(){
		if(triggerPan == true){
			smoothFollow.enabled = false; // Temp. disable smooth follow
			PanCamera(activePlayer_go, inactivePlayer_go);
		}
	}

	public void TriggerPan(GameObject in_activePlayer_go, GameObject in_inactivePlayer_go){
		activePlayer_go = in_activePlayer_go;
		inactivePlayer_go = in_inactivePlayer_go;
		triggerPan = true;
	}

	// Given a reference to the player gameobject we want to pan to and the player
	// that we are paning away from (active and inactive respectively) pan!
	private void PanCamera(GameObject activePlayer_go, GameObject inactivePlayer_go){
		direction = CalculatePanDirection(activePlayer_go.transform, inactivePlayer_go.transform);
		playerDistance = CalculatePlayerDistance(activePlayer_go.transform, inactivePlayer_go.transform);
		panSpeed = CalculatePanSpeed(playerDistance);

		if(direction < 0){ // Pan left
			Debug.Log(this.transform.position.x.ToString() + " | " + activePlayer_go.transform.position.x.ToString());
			if(this.transform.position.x - cameraPanBuffer > activePlayer_go.transform.position.x){
				// Linearly interpolate 
				currentXpos = Mathf.Lerp(currentXpos, activePlayer_go.transform.position.x, Time.deltaTime * panSpeed);
				//Debug.Log("Left - " + currentXpos.ToString());
				// Pan camera position
				this.transform.position = new Vector3(currentXpos, this.transform.position.y, this.transform.position.z);
			}else{
				smoothFollow.enabled = true; // Re-enable smooth follow
				triggerPan = false; // Stop panning
			}
		}else if(direction > 0){ // Pan right
			//Debug.Log(this.transform.position.x.ToString() + " | " + activePlayer_go.transform.position.x.ToString());
			if(this.transform.position.x + cameraPanBuffer < activePlayer_go.transform.position.x){
				// Linearly interpolate 
				currentXpos = Mathf.Lerp(currentXpos, activePlayer_go.transform.position.x, Time.deltaTime * panSpeed);
				//Debug.Log("Right - " + currentXpos.ToString());
				// Pan camera position
				this.transform.position = new Vector3(currentXpos, this.transform.position.y, this.transform.position.z);
			}else{
				smoothFollow.enabled = true; // Re-enable smooth follow
				triggerPan = false; // Stop panning
			}
		}
	}

	// Given active/inactive player go's, calculate the direction we pan in (-1 = left, 1 = right)
	private float CalculatePanDirection(Transform activePlayer, Transform inactivePlayer){
		// Get a vector that point from inactive player pos to active player pos (current to target)
		Vector2 heading = activePlayer.position - inactivePlayer.position;

		// Get normalized direction
		float dist = heading.magnitude;
		Vector2 normDir = heading / dist;

		return normDir.x;
	}

	// Give active/inactive player go's, calculate the distance between
	// them to determine camera pan speed.
	private float CalculatePlayerDistance(Transform activePlayer, Transform inactivePlayer){
		return Vector2.Distance(activePlayer.position, inactivePlayer.position);
	}

	// Given the distance between active/inactive players, determine pan speed
	private float CalculatePanSpeed(float distance){
		// Return 10% of distance as the pan speed
		return distance * panSpeedRatio;
	}
}
