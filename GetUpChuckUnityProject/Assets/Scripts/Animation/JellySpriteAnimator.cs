using UnityEngine;
using System.Collections;

public class JellySpriteAnimator : MonoBehaviour
{
	public UnityJellySprite jellySprite;
	SpriteRenderer spriteRenderer;
	
	void Start ()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		if(jellySprite)
		{
			// If the sprite has changed...
			if(jellySprite.m_Sprite != spriteRenderer.sprite)
			{
				// Update the Jelly Sprite with the new sprite frame
				jellySprite.m_Sprite = spriteRenderer.sprite;
				jellySprite.ReInitMaterial();
				jellySprite.UpdateTextureCoords();
			}
		}
	}
}