using UnityEngine;
using System.Collections;

public class ElevatorInCensor : MonoBehaviour {

	void OnTriggerEnter(Collider hit) {
		if(hit.tag == "Player") {
			//StartCoroutine(Elevator.close());
			Elevator.isInside = true;
		}
	}

	void OnTriggerExit(Collider hit) {
		if(hit.tag == "Player") {
			Elevator.isInside = false;
		}
	}
}
