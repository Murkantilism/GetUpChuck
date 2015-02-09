using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))] // This script should be attached to the main camera object
public class CameraSmoothFollow : MonoBehaviour {

	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public float rotationDamping = 10.0f;
	public bool lockRotation;

	MasterCtrl master;

	void Start(){
		master = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterCtrl>();
		target = master.getActivePlayerGO().transform;
	}

	// Update is called once per frame
	void Update () {
		target = master.getActivePlayerGO().transform;
		Vector3 targetPosition = target.TransformPoint(0, height, -distance);
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);

		if(smoothRotation == true){
			Quaternion targetPosQuat = Quaternion.LookRotation(target.position - transform.position, target.up);
			targetPosition = new Vector3(targetPosQuat.x, targetPosQuat.y, targetPosQuat.z);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetPosQuat, Time.deltaTime * rotationDamping);
		}else{
			transform.LookAt(target, target.up);
		}

		if(lockRotation == true){
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
