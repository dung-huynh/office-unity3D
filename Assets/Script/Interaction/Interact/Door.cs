using UnityEngine;
using System.Collections;

public class Door : InteractableObject {

	private float hinge;				//in z-axis

	private static float maxAngle;		//in degrees
	private static float minAngle;		//in degrees

	private bool isOpen;

	void Start() {
		hinge = GetComponent<Transform>().localRotation.z;
		maxAngle = 100;
		minAngle = 0;
		isOpen = false;
	}

	public override void setAction() {
		StopCoroutine("rotate");

		if(isOpen == false) {
			StartCoroutine(rotate(1));
		}
		else {
			StartCoroutine(rotate(-1));
		}
	}

	IEnumerator rotate(int dir) {
		//Lerp or += to rotate
		yield return null;
	}
}
