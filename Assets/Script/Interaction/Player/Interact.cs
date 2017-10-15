using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

	public Texture2D crosshairTexture;
	private static Rect position;

	void Start() {
		if(crosshairTexture != null) {
			position = new Rect((Screen.width/2) - (crosshairTexture.width/2), (Screen.height/2) - (crosshairTexture.height/2), crosshairTexture.width, crosshairTexture.height);
		}
	}

	void Update() {
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.yellow);
		if(Physics.Raycast(ray, out hit, 1.5f) && hit.transform.tag == "Interactable") {
			//Debug.Log("Mouse in on button");
			if(Input.GetMouseButtonDown(0)) {
				//Debug.Log("Pressed");
				hit.transform.GetComponent<InteractableObject>().setAction();
			}
		}
	}

	void OnGUI() {
		if(crosshairTexture != null) {
			GUI.DrawTexture(position, crosshairTexture);
		}
	}
}