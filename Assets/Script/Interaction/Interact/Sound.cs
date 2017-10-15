using UnityEngine;
using System.Collections;

public class Sound : InteractableObject {

	public override void setAction() {
		if(GetComponent<AudioSource>().clip != null) {
			GetComponent<AudioSource>().Play();
		}
		else {
			Debug.Log("No AudioClip is found in " + name);
		}
	}
}
