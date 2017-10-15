using UnityEngine;
using System.Collections;

public class LightSwitch : InteractableObject {

	public override void setAction() {
		GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
	}
}
