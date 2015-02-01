using UnityEngine;
using System.Collections;

public class CkPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCollisionEnter(Collision bump){
		if (bump.gameObject.tag.Equals ("redCK") || bump.gameObject.tag.Equals ("blueCK")) {
			Player tempP;
			tempP = bump.gameObject.gameObject.GetComponent<Player>();
			tempP.setCkPt(this.transform.position.x, this.transform.position.y);
				}

		}
}
