using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField]
	new MapCamera camera;
	[SerializeField]
	TargetMovement target;

	void Start () {
		camera = FindObjectOfType <MapCamera> ();
		target = FindObjectOfType <TargetMovement> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.transform.position = transform.GetChild (0).position;
			camera.Move (gameObject);
			target.Move (gameObject);
		}
	}
}
