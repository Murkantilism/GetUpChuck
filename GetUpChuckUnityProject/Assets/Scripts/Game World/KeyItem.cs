using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour {

	private Image currentImage;
	private Sprite mySprite;
	private HUD hud;

	void Start () {
		hud = GameObject.Find("UI_Handler").GetComponent<HUD>();
		currentImage = GameObject.Find ("KeyItemSlot").GetComponent<Image> ();
		currentImage.enabled = false;
		mySprite = this.gameObject.GetComponent<SpriteRenderer> ().sprite;
	}

	public void PickUp() {
		currentImage.sprite = mySprite;
		currentImage.enabled = true;
		hud.ItemPickup ();

	}
	public void Discard() {
		hud.ItemDiscard ();
		currentImage.enabled = false;
	}

}
