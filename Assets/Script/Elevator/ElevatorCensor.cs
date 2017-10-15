using UnityEngine;
using System.Collections;

public class ElevatorCensor : MonoBehaviour {

	void OnTriggerEnter(Collider hit) {
		if(hit.tag == "Player" && Elevator.isClosed) {
			StartCoroutine(Elevator.open());
		}
	}

	void OnTriggerExit(Collider hit) {
		if(hit.tag == "Player" && !Elevator.isClosed) {
			StartCoroutine(Elevator.close());
		}
	}
}