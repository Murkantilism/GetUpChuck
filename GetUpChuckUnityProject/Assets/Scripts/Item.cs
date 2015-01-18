using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	//stores weight of item
	public int weight;

	//stores hue
	//red = 1
	//blue = 2
	public int hue;

	//name of object
	public string name;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//geter and seter for weight
	public int getWeight (){
		return this.weight;
		}

	public void setWeight(int inWeight){
		this.weight = inWeight;
		}

	//geter and seter for hue
	public int getHue(){
		return this.hue;
		}

	public void setHue (string tmpHue){
		if (tmpHue.Equals ("red")) {
			this.hue = 1;
				}
		if (tmpHue.Equals ("blue")) {
			this.hue = 2;
				}
		}

	//geter and seter for name
	public string getName(){
		return this.name;
		}

	public void setName(string tmpName){
		this.name = tmpName;
	}

}
