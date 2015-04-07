using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour, Obstacle {

	public bool inPit = false;
	public GameObject spawnPoint;
	public GameObject chuck_red;
	public GameObject chuck_blue;
	JellySprite chuck_red_jelly;
	JellySprite chuck_blue_jelly;

	void Start(){
		chuck_red = GameObject.Find("redPlayer");
		chuck_blue = GameObject.Find("bluePlayer");
		chuck_red_jelly = chuck_red.GetComponent<JellySprite>();
		chuck_blue_jelly = chuck_blue.GetComponent<JellySprite>();
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("red")){
			condResponse(chuck_red_jelly); 
		}else if(coll.gameObject.tag.Equals("blue")){
			condResponse(chuck_blue_jelly);
		}
	}

	public void condResponse(){
		return;
	}

	public void condResponse(JellySprite jelly) {
		inPit = true;
		MoveJellySpriteToPosition(jelly, spawnPoint.transform.position, true);
	}

	// Move the given jelly sprite to the given position
	void MoveJellySpriteToPosition(JellySprite jellySprite, Vector3 position, bool resetVelocity)
	{
		Vector3 offset = position - jellySprite.CentralPoint.GameObject.transform.position;
		
		foreach(JellySprite.ReferencePoint referencePoint in jellySprite.ReferencePoints)
		{
			if(!referencePoint.IsDummy)
			{
				referencePoint.GameObject.transform.position = referencePoint.GameObject.transform.position + offset;
				
				if(resetVelocity)
				{
					if(referencePoint.Body2D)
					{
						referencePoint.Body2D.angularVelocity = 0.0f;
						referencePoint.Body2D.velocity = Vector2.zero;
					}
					else if(referencePoint.Body3D)
					{
						referencePoint.Body3D.angularVelocity = Vector3.zero;
						referencePoint.Body3D.velocity = Vector3.zero;
					}
				}
			}
		}
	}
	
	public bool getStatus() {
		return inPit;
	}
}
