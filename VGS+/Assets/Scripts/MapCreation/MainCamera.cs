using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Vector3 cameraPos = new Vector3 (0, 20, 0);
	public Transform target;

	void FixedUpdate () {
		cameraPos = new Vector3 (
		Mathf.SmoothStep (transform.position.x, target.transform.position.x, 0.3f), 
		0, 
		Mathf.SmoothStep (transform.position.z, target.transform.position.z, 0.3f));
	}

	void LateUpdate () {
		transform.position = cameraPos + Vector3.down * -10;
	}
}
