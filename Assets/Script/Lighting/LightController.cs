using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	//PUBLIC
	public FLOOR floor;

	//PRIVATE
	private LightSystem system;
	private int selectedFloor;

	void Awake() {
		system = GameObject.Find("LightSystem").GetComponent<LightSystem>();
	}

	void OnEnable() {
		switch(floor) {
		case FLOOR.First:
			selectedFloor = 0;
			break;
		case FLOOR.Second:
			selectedFloor = 1;
			break;
		case FLOOR.Third:
			selectedFloor = 2;
			break;
		}
	}

	void OnTriggerEnter(Collider hit) {
		if(hit.transform.tag == "Player") {
			system.switchLights(selectedFloor);
		}
	}
}
