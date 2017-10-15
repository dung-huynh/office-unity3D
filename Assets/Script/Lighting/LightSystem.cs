using UnityEngine;
using System.Collections;

public class LightSystem : MonoBehaviour {
	//PUBLIC
	public GameObject[] lightGroup;

	public void switchLights(int selectedFloor) {
		for(int floorIndex = 0; floorIndex < lightGroup.Length; floorIndex++) {
			if(floorIndex != selectedFloor) {
				lightGroup[floorIndex].SetActive(false);
			}
			else {
				lightGroup[floorIndex].SetActive(true);
			}
		}
	}
}
