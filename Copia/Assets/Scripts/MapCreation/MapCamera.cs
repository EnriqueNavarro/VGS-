using UnityEngine;
using System.Collections;

public class MapCamera : MonoBehaviour
{ 
	[SerializeField]
	private Vector3 jump = new Vector3 (16, 0, 8);
	private Vector3 camera;

	void Start () {
		camera = new Vector3 (transform.position.x, 0, transform.position.z);
	}

	void Update () {
		transform.position = new Vector3 (camera.x, camera.y, camera.z);
	}

	public void Move (GameObject door) {
		if (door.CompareTag("DoorUp")) {
			camera.z += jump.z; 
		} else if (door.CompareTag("DoorDown")) {
			camera.z -= jump.z;
		} else if (door.CompareTag("DoorRight")) {
			camera.x += jump.x;
		} else if (door.CompareTag("DoorLeft")) {
			camera.x -= jump.x;
		} 
	}
}

