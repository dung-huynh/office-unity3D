using UnityEngine;
using System.Collections;

public enum FLOOR {First,Second,Third};

public class Elevator : InteractableObject {
	//ENUM
	public FLOOR floor;

	//STATIC
	private static float[] floorPosition = new float[3]; 				//stores the y-coordinate of each floor where elevator will land
	private static float doorL_in,doorL_out,doorR_in,doorR_out;			//stores the x-coordinate of each door
	private static bool isGoing;										//stores the condition whether the elevator is moving or not
	public static bool isClosed;										//stores the conditoin whether the doors are closed or not
	public static bool isInside;										//stores the condition whether the player is inside the elevator or not

	//GAMEOBJECT && CUSTOM CLASS
	private static GameObject elevator;
	private static GameObject doorL,doorR;
	public static GameObject player;
	private LightSystem system;

	//VARIABLES
	private int selectedFloor; 											//stores the ongoing floor
	public bool isOpenButton;											//for open button
	public bool isCloseButton;											//for close button

	void OnEnable() {
		//Setup selected floor corresponding to button
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

	void Awake() {
		player = GameObject.Find("Player");
		system = GameObject.Find("Lighting").GetComponent<LightSystem>();

		//Player starts outside of elevator
		isInside = false;

		//Setup doors' position for movement
		doorL_in = -0.5f;
		doorR_in = 0.48f;
		doorL_out = -1f;
		doorR_out = 0.98f;

		//Assigns doors to gameObjects
		doorL = transform.parent.parent.FindChild("doorL").gameObject;
		doorR = transform.parent.parent.FindChild("doorR").gameObject;

		//The elevator starts staying
		isGoing = false;

		//The doors close
		isClosed = true;

		//Setup destination
		floorPosition[0] = 1.79f;
		floorPosition[1] = 5f;
		floorPosition[2] = 8.201f;

		//Individual Button -> Buttons Group -> Elevator
		elevator = transform.parent.parent.gameObject;
	}

	/*************
	 * -----------
	 * | BUTTONS |
	 * -----------
	 * ***********/
	public override void setAction() {
		//Debug.Log("isInside: " + isInside);
		//Debug.Log("isGoing: " + isGoing);

		if(!isGoing && isInside) {
			if(!isOpenButton && !isCloseButton) {
				//Stops player's movement
				player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
				player.transform.parent = elevator.transform;

				StartCoroutine(elevate());
			}
			else {
				if(isOpenButton && isClosed) {
					StartCoroutine(open());
				}
				else if(isCloseButton && !isClosed) {
					StartCoroutine(close());
				}
			}
		}
	}

	/*************************
	 * -----------------------
	 * | ELEVATOR'S MOVEMENT |
	 * -----------------------
	 * ***********************/
	IEnumerator elevate() {
		//The elvator starts to move
		isGoing = true;

		//Turns off lights of invisible floor
		system.switchLights(selectedFloor);

		while(elevator.transform.position.y < floorPosition[selectedFloor] - 0.02f || elevator.transform.position.y > floorPosition[selectedFloor] + 0.02f) {
			elevator.transform.position = new Vector3(elevator.transform.position.x, 
				Mathf.Lerp(elevator.transform.position.y, floorPosition[selectedFloor], Time.deltaTime), elevator.transform.position.z);
			yield return null;
		}

		//Opens the door when it arrives
		if(isInside) {
			//Starts player's movement
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			player.transform.parent = null;

			//The elevator stops so it is not moving
			isGoing = false;
		}
		StartCoroutine(open());
	}

	public static IEnumerator open() {
		//Waits for two seconds before opening
		yield return new WaitForSeconds(1);

		while(doorR.transform.localPosition.x < doorR_out - 0.005f) {
			doorL.transform.localPosition = new Vector3(Mathf.Lerp(doorL.transform.localPosition.x, doorL_out, 2f * Time.deltaTime), 
				doorL.transform.localPosition.y, doorL.transform.localPosition.z);
			doorR.transform.localPosition = new Vector3(Mathf.Lerp(doorR.transform.localPosition.x, doorR_out, 2f * Time.deltaTime), 
				doorR.transform.localPosition.y, doorR.transform.localPosition.z);
			yield return null;
		}

		isClosed = false;
	}

	public static IEnumerator close() {
		//Waits for two seconds before closing
		yield return new WaitForSeconds(1);

		while(doorR.transform.localPosition.x > doorR_in + 0.005f) {
			doorL.transform.localPosition = new Vector3(Mathf.Lerp(doorL.transform.localPosition.x, doorL_in, 2f * Time.deltaTime), 
				doorL.transform.localPosition.y, doorL.transform.localPosition.z);
			doorR.transform.localPosition = new Vector3(Mathf.Lerp(doorR.transform.localPosition.x, doorR_in, 2f * Time.deltaTime), 
				doorR.transform.localPosition.y, doorR.transform.localPosition.z);
			yield return null;
		}

		isClosed = true;
	}
}